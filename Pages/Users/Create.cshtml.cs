using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;

namespace PruebaTecnica.Pages.Users {
    public class CreateModel : PageModel {
        private readonly Data.ApplicationContext _context;

        public CreateModel(Data.ApplicationContext context) {
            _context = context;
        }

        public IActionResult OnGet() {
            return Page();
        }

        [BindProperty]
        public User User { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync(string roleStr, string FullName, string Email, string Pass,
    int? Age, string ContactNumber, int? ELevel, string WorkXP,
    string Location, string Industry, int? EmployeeCount) {
            int uRole = (roleStr == "Seeker") ? 1 : 2;

            var outputId = new SqlParameter {
                ParameterName = "@NewUserId",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
        "EXEC sp_RegisterFullUser @FullName, @Email, @Pass, @URole, @Age, @ContactNumber, @ELevel, @WorkXP, NULL, @Location, @Industry, @EmployeeCount, @NewUserId OUTPUT",
                    new SqlParameter("@FullName", FullName),
                    new SqlParameter("@Email", Email),
                    new SqlParameter("@Pass", Pass),
                    new SqlParameter("@URole", uRole),
                    new SqlParameter("@Age", (object)Age ?? DBNull.Value),
                    new SqlParameter("@ContactNumber", (object)ContactNumber ?? DBNull.Value),
                    new SqlParameter("@ELevel", (object)ELevel ?? DBNull.Value),
                    new SqlParameter("@WorkXP", (object)WorkXP ?? DBNull.Value),
                    new SqlParameter("@Location", (object)Location ?? DBNull.Value),
                    new SqlParameter("@Industry", (object)Industry ?? DBNull.Value),
                    new SqlParameter("@EmployeeCount", (object)EmployeeCount ?? DBNull.Value),
                    outputId
            );
            // 3. Obtener el ID generado
            int newId = (int)outputId.Value;

            // 4. INICIAR SESIÓN (Login Automático)
            // Guardamos los datos mínimos necesarios en la sesión
            HttpContext.Session.SetInt32("UserId", newId);
            HttpContext.Session.SetInt32("UserRole", uRole);
            HttpContext.Session.SetString("UserName", FullName);

            // 5. Redirección basada en el rol
            string redirect = "/Index";
            if (uRole == 1) redirect = "/Seeker/Dashboard";
            else if (uRole == 2) redirect = "/Employer/EDashboard";

            return RedirectToPage(redirect);
        }
    }
}
