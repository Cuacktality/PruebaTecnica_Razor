using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Data;
using PruebaTecnica.Models;

namespace PruebaTecnica.Pages.Employer {
    public class EDashboardModel : PageModel {
        private readonly ApplicationContext _context;
        public EDashboardModel(ApplicationContext context) => _context = context;

        public EmployerProfile Profile { get; set; }
        public List<JobOffer> MyJobs { get; set; } 

        public async Task OnGetAsync() {
            int? userId = HttpContext.Session.GetInt32("UserId"); 
            Profile = await _context.EmployerProfile.Include(s => s.User).FirstOrDefaultAsync(m => m.UserId == userId);

            MyJobs = await _context.JobOffer
        .Include(j => j.Employer)          // Carga el EmployerProfile
            .ThenInclude(e => e.User)      // De ese Employer, carga su User
        .OrderByDescending(j => j.CreatedDate)
        .ToListAsync();
        } 
    }
}
