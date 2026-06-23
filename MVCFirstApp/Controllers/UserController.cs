using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MVCFirstApp.Models;
using MVCFirstApp.Data;

namespace MVCFirstApp.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }


        // عرض المستخدمين
        public IActionResult Create()
        {
            ViewBag.Users = _context.Users.ToList();

            return View();
        }


        // حذف مستخدم
        public IActionResult Delete(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserId == id);

            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }

            return RedirectToAction("Create");
        }


        // عرض صفحة التعديل
        public IActionResult Edit(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserId == id);

            return View(user);
        }


        // حفظ التعديل
        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var oldUser = _context.Users.FirstOrDefault(x => x.UserId == user.UserId);

            if (oldUser != null)
            {
                oldUser.UserName = user.UserName;
                oldUser.Email = user.Email;
                oldUser.Password = user.Password;
                oldUser.RoleId = user.RoleId;

                _context.SaveChanges();
            }

            return RedirectToAction("Create");
        }


        // إضافة مستخدم
        [HttpPost]
        public IActionResult Create(User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Users = _context.Users.ToList();
                    return View(user);
                }


                if (!user.Email.Contains("@gmail.com"))
                {
                    ViewBag.EmailError = "Please enter a valid email";
                    ViewBag.Users = _context.Users.ToList();
                    return View(user);
                }


                if (user.UserName.Contains("@"))
                {
                    ViewBag.UserError = "User Name cannot be an email";
                    ViewBag.Users = _context.Users.ToList();
                    return View(user);
                }


                if (user.RoleId < 1 || user.RoleId > 3)
                {
                    ViewBag.RoleError = "Role Id must be between 1 and 3";
                    ViewBag.Users = _context.Users.ToList();
                    return View(user);
                }


                _context.Users.Add(user);
                _context.SaveChanges();


                return RedirectToAction("Create");
            }
            catch
            {
                ViewBag.Users = _context.Users.ToList();
                return View(user);
            }
        }


        public IActionResult Logout()
        {
            return RedirectToAction("Login", "Login");
        }
    }
}