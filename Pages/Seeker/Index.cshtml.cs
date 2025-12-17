using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;

namespace PruebaTecnica.Pages.Seeker
{
    public class IndexModel : PageModel
    {
        private readonly Data.ApplicationContext _context;

        public IndexModel(Data.ApplicationContext context)
        {
            _context = context;
        }

        public IList<SeekerProfile> SeekerProfile { get;set; } = default!;

        public async Task OnGetAsync()
        {
            SeekerProfile = await _context.SeekerProfile
                .Include(s => s.User).ToListAsync();
        }
    }
}
