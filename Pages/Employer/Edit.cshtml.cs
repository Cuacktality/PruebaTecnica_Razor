using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;

namespace PruebaTecnica.Pages.Employer {
    public class EditModel : PageModel {
        private readonly Data.ApplicationContext _context;

        public EditModel(Data.ApplicationContext context) {
            _context = context;
        }

        [BindProperty]
        public EmployerProfile EmployerProfile { get; set; } = default!;
        /// <summary>
        /// Obtiene los datos de la empresa
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync(int? id) {
            if (id == null) {
                return NotFound();
            }

            var employerprofile = await _context.EmployerProfile.FirstOrDefaultAsync(m => m.Id == id);
            if (employerprofile == null) {
                return NotFound();
            }
            EmployerProfile = employerprofile;
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Email");
            return Page();
        }


        /// <summary>
        /// Método async para actualizar la cuenta de la empresa
        /// </summary>
        /// <returns>Regresa a la página de inicio</returns>
        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) return Page();

            _context.Attach(EmployerProfile).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!EmployerProfileExists(EmployerProfile.Id)) return NotFound();
                else throw;
            } 
            return RedirectToPage("./EDashboard");
        }
        /// <summary>
        /// Método async para eliminar la cuenta de la empresa
        /// </summary>
        /// <returns>Regresa a la página de inicio</returns>
        public async Task<IActionResult> OnPostDeleteAsync() {
            await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC sp_DeleteEmployerProfile {EmployerProfile.Id}");
            HttpContext.Session.Clear();
            return RedirectToPage("/Index");
        }

        private bool EmployerProfileExists(int id) {
            return _context.EmployerProfile.Any(e => e.Id == id);
        }
    }
}
