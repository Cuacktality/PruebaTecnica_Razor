using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;

namespace PruebaTecnica.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly Data.ApplicationContext _context;

        public CreateModel(Data.ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public User User { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        /*public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.User.Add(User);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }*/
        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) {
                return Page();
            }
             
            var newIdParam = new SqlParameter {
                ParameterName = "@NewId",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output
            };
             
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_RegisterUser @FullName, @Email, @Pass, @URole, @NewId OUTPUT",
                new SqlParameter("@FullName", User.FullName),
                new SqlParameter("@Email", User.Email),
                new SqlParameter("@Pass", User.Pass),
                new SqlParameter("@URole", (int)User.URole),
                newIdParam
            );
             
            int newlyCreatedId = (int)newIdParam.Value;
             
            if (User.URole == MyRole.Seeker) {
                return RedirectToPage("/SeekerProfiles/Create", new { userId = newlyCreatedId });
            }
            else if (User.URole == MyRole.Employer) {
                return RedirectToPage("/EmployerProfiles/Create", new { userId = newlyCreatedId });
            }

            return RedirectToPage("./Index");
        }
    }
}
