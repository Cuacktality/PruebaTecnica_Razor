using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;

namespace PruebaTecnica.Pages.Employer
{
    public class IndexModel : PageModel
    {
        private readonly Data.ApplicationContext _context;

        public IndexModel(Data.ApplicationContext context)
        {
            _context = context;
        }

        public IList<EmployerProfile> EmployerProfile { get;set; } = default!;

        public async Task OnGetAsync()
        {
            EmployerProfile = await _context.EmployerProfile
                .Include(e => e.User).ToListAsync();
        }
    }
}
