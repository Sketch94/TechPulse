using Microsoft.AspNetCore.Mvc;
using TechPulse.Data;
using TechPulse.Models;
using System.Linq;

namespace TechPulse.Controllers
{
    public class LoginController : Controller
    {
        private readonly TechPulseDbContext _context;

        public LoginController(TechPulseDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users
                    .FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);

                if (user != null)
                {
                    // Add login logic here, e.g., set session/cookie
                    // Redirect to profile or dashboard
                    return RedirectToAction("Profile", "Home");
                }

                TempData["Message"] = "Ogiltigt användarnamn eller lösenord.";
                return View(model); // Return back to the login page with the message
            }

            return View(model);
        }
    }
}
