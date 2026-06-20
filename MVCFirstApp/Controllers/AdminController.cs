using Microsoft.AspNetCore.Mvc;

namespace MVCFirstApp.Controllers
{
    public class AdminController : Controller
    {
      

        public IActionResult Dashboard()
        {
            return View();
        }
    }
}