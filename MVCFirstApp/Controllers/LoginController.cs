using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MVCFirstApp.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public IActionResult CheckLogin(string Email, string Password)
        {
            
            var user = UserController.users
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