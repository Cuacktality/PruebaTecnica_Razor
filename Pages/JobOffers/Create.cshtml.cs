using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;

namespace PruebaTecnica.Pages.JobOffers {
    public class CreateModel : PageModel {
        private readonly Data.ApplicationContext _context;
        public CreateModel(Data.ApplicationContext context) => _context = context;
        [BindProperty] public JobOffer JobOffer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync() {
            int? userId = HttpContext.Session.GetInt32("UserId");
            var employer = await _context.EmployerProfile.FirstOrDefaultAsync(e => e.UserId == userId);

            if (employer == null) return RedirectToPage("/Index");

            JobOffer = new JobOffer {
                EmployerId = employer.Id,
                CreatedDate = DateTime.Now
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync() {
            ModelState.Remove("JobOffer.Employer");

            if (!ModelState.IsValid) {
                return Page();
            }

            _context.JobOffer.Add(JobOffer);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Employer/EDashboard");
        }
    }
}