using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;

namespace PruebaTecnica.Pages.Employer
{
    public class DetailsModel : PageModel
    {
        private readonly Data.ApplicationContext _context;

        public DetailsModel(Data.ApplicationContext context)
        {
            _context = context;
        }

        public EmployerProfile EmployerProfile { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employerprofile = await _context.EmployerProfile.FirstOrDefaultAsync(m => m.Id == id);
            if (employerprofile == null)
            {
                return NotFound();
            }
            else
            {
                EmployerProfile = employerprofile;
            }
            return Page();
        }
    }
}
