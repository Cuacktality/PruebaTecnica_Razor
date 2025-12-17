using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;

namespace PruebaTecnica.Pages.JobApplications
{
    public class DetailsModel : PageModel
    {
        private readonly Data.ApplicationContext _context;

        public DetailsModel(Data.ApplicationContext context)
        {
            _context = context;
        }

        public JobApplication JobApplication { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobapplication = await _context.JobApplication.FirstOrDefaultAsync(m => m.Id == id);
            if (jobapplication == null)
            {
                return NotFound();
            }
            else
            {
                JobApplication = jobapplication;
            }
            return Page();
        }
    }
}
