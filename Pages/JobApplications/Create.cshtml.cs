using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PruebaTecnica.Models;

namespace PruebaTecnica.Pages.JobApplications
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
        ViewData["JobOfferId"] = new SelectList(_context.JobOffer, "Id", "Description");
        ViewData["SeekerId"] = new SelectList(_context.SeekerProfile, "Id", "ContactNumber");
            return Page();
        }

        [BindProperty]
        public JobApplication JobApplication { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.JobApplication.Add(JobApplication);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
