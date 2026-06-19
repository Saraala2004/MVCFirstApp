using Microsoft.AspNetCore.Mvc;

namespace MVCFirstApp.Controllers
{
    public class ManagerController : Controller
    {
        public IActionResult Manager()
        {
            return View();
        }
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}