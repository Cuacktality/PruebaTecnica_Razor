using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;

namespace PruebaTecnica.Pages.Seeker
{
    public class EditModel : PageModel
    {
        private readonly Data.ApplicationContext _context;

        public EditModel(Data.ApplicationContext context)
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

            var seekerprofile =  await _context.SeekerProfile.FirstOrDefaultAsync(m => m.Id == id);
            if (seekerprofile == null)
            {
                return NotFound();
            }
            SeekerProfile = seekerprofile;
           ViewData["UserId"] = new SelectList(_context.User, "Id", "Email");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(SeekerProfile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeekerProfileExists(SeekerProfile.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Seeker/Dashboard");
        }
        public async Task<IActionResult> OnPostAsync(int? id) {
            if (id == null) {
                return NotFound();
            }

            var seekerprofile = await _context.SeekerProfile.FindAsync(id);
            if (seekerprofile != null) {
                SeekerProfile = seekerprofile;
                _context.SeekerProfile.Remove(SeekerProfile);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
        private bool SeekerProfileExists(int id)
        {
            return _context.SeekerProfile.Any(e => e.Id == id);
        }
    }
}
