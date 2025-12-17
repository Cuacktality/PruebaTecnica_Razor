using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;

namespace PruebaTecnica.Pages.JobOffers
{
    public class DetailsModel : PageModel
    {
        private readonly Data.ApplicationContext _context;

        public DetailsModel(Data.ApplicationContext context)
        {
            _context = context;
        }

        public JobOffer JobOffer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var joboffer = await _context.JobOffer.FirstOrDefaultAsync(m => m.Id == id);
            if (joboffer == null)
            {
                return NotFound();
            }
            else
            {
                JobOffer = joboffer;
            }
            return Page();
        }
    }
}
