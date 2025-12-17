using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;

namespace PruebaTecnica.Pages.Seeker
{
    public class DeleteModel : PageModel
    {
        private readonly Data.ApplicationContext _context;

        public DeleteModel(Data.ApplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SeekerProfile SeekerProfile { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seekerprofile = await _context.SeekerProfile.FirstOrDefaultAsync(m => m.Id == id);

            if (seekerprofile == null)
            {
                return NotFound();
            }
            else
            {
                SeekerProfile = seekerprofile;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seekerprofile = await _context.SeekerProfile.FindAsync(id);
            if (seekerprofile != null)
            {
                SeekerProfile = seekerprofile;
                _context.SeekerProfile.Remove(SeekerProfile);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
