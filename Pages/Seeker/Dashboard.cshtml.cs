using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Data;
using PruebaTecnica.Models;

namespace PruebaTecnica.Pages.Seeker {
    public class SeekerDashboardModel : PageModel {
        private readonly ApplicationContext _context;
        public SeekerDashboardModel(ApplicationContext context) => _context = context;

        public SeekerProfile Profile { get; set; }
        public List<JobOffer> AvailableJobs { get; set; } = new();
        public List<JobApplication> MyApplications { get; set; } = new();
        //public int MyApplicationsCount { get; set; } 

        public async Task OnGetAsync() {
            var userId = HttpContext.Session.GetInt32("UserId");

            Profile = await _context.SeekerProfile.Include(s => s.User).FirstOrDefaultAsync(m => m.UserId == userId);
            //AvailableJobs = await _context.JobOffer.Include(j => j.Employer).ThenInclude(e => e.User).OrderByDescending(j => j.CreatedDate).ToListAsync();
            MyApplications = new List<JobApplication>();
            AvailableJobs = new List<JobOffer>();

            if (Profile == null) return;

            MyApplications = await _context.JobApplication
         .Where(a => a.SeekerId == Profile.Id)
         .Include(a => a.JobOffer).ThenInclude(j => j.Employer).ThenInclude(e => e.User)
         .OrderByDescending(a => a.AppliedAt)
         .ToListAsync();

            var appliedIds = MyApplications.Select(a => a.JobOfferId).ToList();

            AvailableJobs = await _context.JobOffer
            .Include(j => j.Employer).ThenInclude(e => e.User)
            .Where(j => !appliedIds.Contains(j.Id))
            .OrderByDescending(j => j.CreatedDate)
            .ToListAsync();

        }
        public async Task<IActionResult> OnPostApplyAsync(int jobId) {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId.Equals(0)) return RedirectToPage("/Index");

            var seeker = await _context.SeekerProfile.FirstOrDefaultAsync(s => s.UserId == userId);
            if (seeker == null) return NotFound("Candidato no existe");

            bool IsApplied = await _context.JobApplication.AnyAsync(a => a.JobOfferId == jobId && a.SeekerId == seeker.Id);

            if (!IsApplied) {
                var apply = new JobApplication {
                    JobOfferId = jobId,
                    SeekerId = seeker.Id,
                    AppliedAt = DateTime.Now,
                    Status = MyStatus.Sended
                };

                _context.JobApplication.Add(apply);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}
