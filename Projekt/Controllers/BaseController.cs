using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Projekt.Data;
using Projekt.Models;

namespace Projekt.Controllers
{
    public class BaseController : Controller
    {

        protected readonly ProjektContext _context;
        protected readonly UserManager<ApplicationUser> _userManager;

        public BaseController(UserManager<ApplicationUser> userManager, ProjektContext context)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            // Pobranie ID użytkownika
            var userId = _userManager.GetUserId(User);

            // Pobranie całego obiektu użytkownika
            var user = await _userManager.GetUserAsync(User);

            // Teraz możesz np. używać user.Email, user.UserName itp.
            return View(user);
        }
        protected IActionResult CheckSession()
        {
            if (HttpContext.Session.GetInt32("UserId").HasValue)
            {
                ViewData["Username"] = HttpContext.Session.GetString("Username");
                return RedirectToAction("Logged", "Login");
            }

            return null;
        }
    }
}
