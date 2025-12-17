using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;

namespace PruebaTecnica.Pages.JobOffers
{
    public class IndexModel : PageModel
    {
        private readonly Data.ApplicationContext _context;

        public IndexModel(Data.ApplicationContext context)
        {
            _context = context;
        }

        public IList<JobOffer> JobOffer { get;set; } = default!;

        public async Task OnGetAsync()
        {
            JobOffer = await _context.JobOffer
                .Include(j => j.Employer).ToListAsync();
        }
    }
}
