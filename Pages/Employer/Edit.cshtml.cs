using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;

namespace PruebaTecnica.Pages.Employer
{
    public class EditModel : PageModel
    {
        private readonly Data.ApplicationContext _context;

        public EditModel(Data.ApplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public EmployerProfile EmployerProfile { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employerprofile =  await _context.EmployerProfile.FirstOrDefaultAsync(m => m.Id == id);
            if (employerprofile == null)
            {
                return NotFound();
            }
            EmployerProfile = employerprofile;
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

            _context.Attach(EmployerProfile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployerProfileExists(EmployerProfile.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EmployerProfileExists(int id)
        {
            return _context.EmployerProfile.Any(e => e.Id == id);
        }
    }
}
