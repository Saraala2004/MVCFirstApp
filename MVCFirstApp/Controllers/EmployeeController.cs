using Microsoft.AspNetCore.Mvc;

namespace MVCFirstApp.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Employee()
        {
            return View();
        }
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}