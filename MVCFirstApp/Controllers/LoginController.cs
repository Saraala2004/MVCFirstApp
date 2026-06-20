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
            // التشييك على قائمة المستخدمين
            var user = UserController.users
                .FirstOrDefault(x => x.Email == Email && x.Password == Password);

            if (user == null)
            {
                ViewBag.Error = "Invalid Email or Password";
                return View("Login");
            }

            // توجيه الأدمن (Role 1) إلى الداشبورد الخاصة به
            if (user.RoleId == 1)
            {
                return RedirectToAction("Dashboard", "Admin");
            }

            // توجيه الموظف (Role 2) إلى واجهة الموظف
            if (user.RoleId == 2)
            {
                return RedirectToAction("Employee", "Employee"); // غيريها لـ Dashboard لو كان هذا اسم دالته
            }

            // توجيه المدير (Role 3) إلى الداشبورد الخاصة به
            if (user.RoleId == 3)
            {
                return RedirectToAction("Dashboard", "Manager");
            }

            return View("Login");
        }
    }
}