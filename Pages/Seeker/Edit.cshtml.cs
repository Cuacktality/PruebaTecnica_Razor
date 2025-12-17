using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;

namespace PruebaTecnica.Pages.Seeker {
    public class EditModel : PageModel {
        private readonly Data.ApplicationContext _context;

        public EditModel(Data.ApplicationContext context) {
            _context = context;
        }

        [BindProperty]
        public SeekerProfile SeekerProfile { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id) {
            if (id == null) return NotFound();
             
            var seekerprofile = await _context.SeekerProfile
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (seekerprofile == null) return NotFound();

            SeekerProfile = seekerprofile;
            return Page();
        } 
        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) return Page();

            _context.Attach(SeekerProfile).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!SeekerProfileExists(SeekerProfile.Id)) return NotFound();
                else throw;
            } 
            return RedirectToPage("./Dashboard");
        }
         
        public async Task<IActionResult> OnPostDeleteAsync() {
            if (SeekerProfile == null || SeekerProfile.Id <= 0) return NotFound();

            try {
                // Ejecutamos el Stored Procedure
                await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC sp_DeleteSeekerProfile {SeekerProfile.Id}");

                // Limpiamos la sesión tras borrar la cuenta
                HttpContext.Session.Clear();

                return RedirectToPage("/Index");
            }
            catch (Exception ex) {
                // Opcional: Manejar el error o enviarlo a la vista
                ModelState.AddModelError(string.Empty, $"Error al eliminar el perfil: {ex.Message}");
                return Page();
            }
        }

        private bool SeekerProfileExists(int id) {
            return _context.SeekerProfile.Any(e => e.Id == id);
        }
    }
}