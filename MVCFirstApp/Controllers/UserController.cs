using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MVCFirstApp.Models;

namespace MVCFirstApp.Controllers
{
    public class UserController : Controller
    {
        public static List<User> users = new List<User>();
        public IActionResult Index()
        {
            return View();
        }

        // GET
        public IActionResult Create()
        {
            ViewBag.Users = users;

            return View();
        }
        public IActionResult Delete(int id)
        {
            var user = users.FirstOrDefault(x => x.UserId == id);

            if (user != null)
            {
                users.Remove(user);
            }

            return RedirectToAction("Create");
        }
        public IActionResult Edit(int id)
        {
            var user = users.FirstOrDefault(x => x.UserId == id);

            return View(user);
        }
        [HttpPost]
        public IActionResult Edit(User user)
        {
            var oldUser = users.FirstOrDefault(x => x.UserId == user.UserId);

            if (oldUser != null)
            {
                oldUser.UserName = user.UserName;
                oldUser.Email = user.Email;
                oldUser.Password = user.Password;
                oldUser.RoleId = user.RoleId;
            }

            return RedirectToAction("Create");
        }

        // POST
        [HttpPost]
        public IActionResult Create(User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(user);
                }

                if (!user.Email.Contains("@gmail.com"))
                {
                    ViewBag.EmailError = "Please enter a valid email";
                    return View(user);
                }

                if (user.UserName.Contains("@"))
                {
                    ViewBag.UserError = "User Name cannot be an email";
                    return View(user);
                }

                if (user.RoleId < 1 || user.RoleId > 3)
                {
                    ViewBag.RoleError = "Role Id must be between 1 and 3";
                    return View(user);
                }

                users.Add(user);

                return RedirectToAction("Create");
            }

            catch
            {
                return View(user);
            }
        }
        public IActionResult Logout()
        {
            return RedirectToAction("Login", "Login");
        }
        
    }
}