using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Data;
using PruebaTecnica.Models;

namespace PruebaTecnica.Controllers {
    public class AccountController : Controller {
        private readonly ApplicationContext _context;

        public AccountController(ApplicationContext context) {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password) {
            // Ejecutamos el SP y obtenemos el resultado
            var users = await _context.User
        .FromSqlRaw("EXEC sp_ValidateLogin @Email, @Pass",
            new SqlParameter("@Email", email),
            new SqlParameter("@Pass", password))
        .ToListAsync();
            var user = users.FirstOrDefault();
            if (user != null) {
                HttpContext.Session.SetString("UserName", user.FullName);
                HttpContext.Session.SetInt32("UserRole", (int)user.URole);
                HttpContext.Session.SetInt32("UserId", user.Id);

                if (user.URole == MyRole.Seeker) return RedirectToPage("/Seeker/Dashboard"); // Candidatos van a ver vacantes
                else return RedirectToPage("/Employer/EDashboard", new { id = user.Id }); // Empresas a su perfil
            }

            // Si falla, volvemos al Home con un error
            ViewData["Error"] = "Credenciales incorrectas";
            return RedirectToPage("/Index");
        }

        public IActionResult Logout() {
            HttpContext.Session.Clear();
            return RedirectToPage("/Index");
        }
    }
}