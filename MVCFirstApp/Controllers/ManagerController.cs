using Microsoft.AspNetCore.Mvc;

namespace MVCFirstApp.Controllers
{
    public class ManagerController : Controller
    {
        
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult TasksStatistics()
        {
            return View();
        }
        public IActionResult AssignTask()
        {
            return View();
        }
        public IActionResult EmployeeReports()
        {
            return View();
        }
    }
}