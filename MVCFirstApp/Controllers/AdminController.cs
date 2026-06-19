using Microsoft.AspNetCore.Mvc;

namespace MVCFirstApp.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Admin()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }
    }
}