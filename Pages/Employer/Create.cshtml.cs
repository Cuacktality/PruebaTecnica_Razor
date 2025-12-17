using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PruebaTecnica.Models;

namespace PruebaTecnica.Pages.Employer
{
    public class CreateModel : PageModel
    {
        private readonly Data.ApplicationContext _context;

        public CreateModel(Data.ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["UserId"] = new SelectList(_context.User, "Id", "Email");
            return Page();
        }

        [BindProperty]
        public EmployerProfile EmployerProfile { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.EmployerProfile.Add(EmployerProfile);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
