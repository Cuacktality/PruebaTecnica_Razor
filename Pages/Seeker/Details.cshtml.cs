using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;

namespace PruebaTecnica.Pages.Seeker
{
    public class DetailsModel : PageModel
    {
        private readonly Data.ApplicationContext _context;

        public DetailsModel(Data.ApplicationContext context)
        {
            _context = context;
        }

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
    }
}
