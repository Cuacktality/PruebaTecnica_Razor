using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;

namespace PruebaTecnica.Pages.JobOffers {
    public class ApplicationsModel : PageModel {
        private readonly Data.ApplicationContext _context;
        public ApplicationsModel(Data.ApplicationContext context) => _context = context;

        public JobOffer JobOffer { get; set; } = default!;
        public List<JobApplication> Applications { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int jobId) {
            JobOffer = await _context.JobOffer.FirstOrDefaultAsync(j => j.Id == jobId);

            if (JobOffer == null) return NotFound();

            Applications = await _context.JobApplication
               .Include(a => a.Seeker)
                   .ThenInclude(s => s.User)
               .Where(a => a.JobOfferId == jobId)
               .OrderByDescending(a => a.AppliedAt)
               .ToListAsync();

            return Page();
        }
    }
}
