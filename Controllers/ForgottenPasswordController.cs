using Microsoft.AspNetCore.Mvc;
using TechPulse.Data;
using TechPulse.Models;

namespace TechPulse.Controllers
{
    public class ForgottenPasswordController : Controller
    {
        private readonly TechPulseDbContext _context;

        public ForgottenPasswordController(TechPulseDbContext context)
        {
            _context = context;
        }

        // GET: ForgottenPassword
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // POST: ForgottenPassword
        [HttpPost]
        public IActionResult Index(User user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);
                if (existingUser != null)
                {
                    ViewBag.Message = "Ett e-postmeddelande har skickats för att återställa ditt lösenord.";
                }
                else
                {
                    ViewBag.Message = "Ingen användare hittades med den angivna e-postadressen.";
                }

                return View(new User()); // Visa en tom form igen
            }

            return View(user);
        }
    }
}
