using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;

namespace PruebaTecnica.Pages.Seeker {
    public class DetailsModel : PageModel {
        private readonly Data.ApplicationContext _context;

        public DetailsModel(Data.ApplicationContext context) {
            _context = context;
        }

        public SeekerProfile SeekerProfile { get; set; } = default!;
        public MyStatus CurrentStatus { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id) {
            if (id == null) return NotFound();

            var seekerprofile = await _context.SeekerProfile
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (seekerprofile == null) return NotFound();
            SeekerProfile = seekerprofile;

            var application = await _context.JobApplication
                .FirstOrDefaultAsync(a => a.SeekerId == id);

            if (application != null) {
                if (application.Status == MyStatus.Sended || application.Status == MyStatus.None) {
                    application.Status = MyStatus.Viewed;
                    await _context.SaveChangesAsync();
                }

                CurrentStatus = application.Status;
            }

            return Page();
        }
        public async Task<IActionResult> OnPostUpdateStatusAsync(int seekerId, int newStatus) {

            var application = await _context.JobApplication
                .FirstOrDefaultAsync(a => a.SeekerId == seekerId);

            if (application != null) {

                application.Status = (MyStatus)newStatus;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage(new { id = seekerId });
        }
    }
}