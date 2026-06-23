using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MVCFirstApp.Data;

namespace MVCFirstApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }


        [HttpPost]
        public IActionResult CheckLogin(string Email, string Password)
        {
            var user = _context.Users
                .FirstOrDefault(x => x.Email == Email && x.Password == Password);


            if (user == null)
            {
                ViewBag.Error = "Invalid Email or Password";
                return View("Login");
            }


            if (user.RoleId == 1)
            {
                return RedirectToAction("Dashboard", "Admin");
            }


            if (user.RoleId == 2)
            {
                return RedirectToAction("Dashboard", "Employee");
            }


            if (user.RoleId == 3)
            {
                return RedirectToAction("Dashboard", "Manager");
            }


            return View("Login");
        }
    }
}