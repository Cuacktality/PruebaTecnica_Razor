using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using PruebaTecnica.Models;

namespace PruebaTecnica.Pages {
    public class IndexModel : PageModel {
        [BindProperty] public string Email { get; set; }
        [BindProperty] public string Password { get; set; }

        private readonly ILogger<IndexModel> _logger;

        private readonly Data.ApplicationContext _context;
        public IndexModel(Data.ApplicationContext context) {
            _context = context;
        }

        public void OnGet() {

        }

        public async Task<IActionResult> OnPostLoginAsync() {
            // Ejecutamos tu Stored Procedure
            var user = await _context.User
                .FromSqlRaw("EXEC sp_ValidateLogin @Email, @Pass",
                    new SqlParameter("@Email", Email),
                    new SqlParameter("@Pass", Password))
                .ToListAsync();

            var foundUser = user.FirstOrDefault();

            if (foundUser != null) {
                // Guardamos en sesión
                HttpContext.Session.SetString("UserName", foundUser.FullName);
                HttpContext.Session.SetInt32("UserId", foundUser.Id);
                HttpContext.Session.SetInt32("UserRole", (int)foundUser.URole);

                // Redirección según rol a tus carpetas de Razor Pages
                if (foundUser.URole == MyRole.Seeker) {
                    return RedirectToPage("/Seeker/Details", new { id = foundUser.Id });
                }
                else {
                    return RedirectToPage("/Employer/Details", new { id = foundUser.Id });
                }
            }

            ModelState.AddModelError(string.Empty, "Credenciales inválidas");
            return Page();
        }
    }
}
