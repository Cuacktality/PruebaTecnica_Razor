using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;

namespace PruebaTecnica.Pages.JobApplications
{
    public class IndexModel : PageModel
    {
        private readonly Data.ApplicationContext _context;

        public IndexModel(Data.ApplicationContext context)
        {
            _context = context;
        }

        public IList<JobApplication> JobApplication { get;set; } = default!;

        public async Task OnGetAsync()
        {
            JobApplication = await _context.JobApplication
                .Include(j => j.JobOffer)
                .Include(j => j.Seeker).ToListAsync();
        }
    }
}
