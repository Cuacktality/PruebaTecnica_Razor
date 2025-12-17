using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PruebaTecnica.Models;

namespace PruebaTecnica.Pages.JobOffers
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
        ViewData["EmployerId"] = new SelectList(_context.EmployerProfile, "Id", "Industry");
            return Page();
        }

        [BindProperty]
        public JobOffer JobOffer { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.JobOffer.Add(JobOffer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
