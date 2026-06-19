using Microsoft.AspNetCore.Mvc;
using MVCFirstApp.Controllers;

namespace MVCFirstApp.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CheckLogin(string Email, string Password)
        {
            var user = UserController.users
                .FirstOrDefault(x =>
                    x.Email == Email &&
                    x.Password == Password);

            if (user == null)
            {
                ViewBag.Error = "Invalid Email or Password";
                return View("Login");
            }

            if (user.RoleId == 1)
            {
                return RedirectToAction("Admin", "Admin");
            }

            if (user.RoleId == 2)
            {
                return RedirectToAction("Employee", "Employee");
            }

            if (user.RoleId == 3)
            {
                return RedirectToAction("Manager", "Manager");
            }

            return View("Login");
        }
    }
}