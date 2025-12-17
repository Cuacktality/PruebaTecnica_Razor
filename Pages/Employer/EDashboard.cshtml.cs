using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Data;
using PruebaTecnica.Models;

namespace PruebaTecnica.Pages.Employer {
    public class EDashboardModel : PageModel {
        private readonly ApplicationContext _context;
        public EDashboardModel(ApplicationContext context) => _context = context;

        public EmployerProfile Profile { get; set; }
        public List<JobOffer> MyJobs { get; set; } = new();

        public async Task OnGetAsync() {
            var userId = HttpContext.Session.GetInt32("UserId");
            Profile = await _context.EmployerProfile.Include(s => s.User).FirstOrDefaultAsync(m => m.UserId == userId);

            if (Profile == null) return;

            MyJobs = await _context.JobOffer
                    .Include(j => j.Employer)
                    .ThenInclude(e => e.User)
                    .Include(j => j.Applications)
                    .Where(j => j.EmployerId == Profile.Id)
                    .OrderByDescending(j => j.CreatedDate)
                    .ToListAsync(); 

        }
    }
}
