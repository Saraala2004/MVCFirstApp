using Microsoft.AspNetCore.Mvc;
using MVCFirstApp.Models;
using MVCFirstApp.Data;
namespace MVCFirstApp.Controllers
{
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _context;
        public TaskController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var tasks = _context.TaskItems.ToList();

            ViewBag.NotStartedTasks = tasks.Count(t => t.Status == "Not Started");
            ViewBag.CompletedTasks = tasks.Count(t => t.Status == "Completed");
            ViewBag.InProgressTasks = tasks.Count(t => t.Status == "In Progress");
            ViewBag.DelayedTasks = tasks.Count(t => t.Status == "Delayed");

            UpdateTaskStatus();

            return View(tasks);
        }

        private void UpdateTaskStatus()
        {
            var tasks = _context.TaskItems.ToList();

            foreach (var task in tasks)
            {
                if (task.DueDate.Date < DateTime.Now.Date && task.Status != "Completed")
                {
                    task.Status = "Delayed";
                }
            }

            _context.SaveChanges();
        }

        public IActionResult Create()
        {
            var newtask = new TaskItem();
            newtask.DueDate = DateTime.Now;

            ViewBag.Users = _context.Users.ToList();

            return View(newtask);
        }

        [HttpPost]
        public IActionResult Create(TaskItem task)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Users = _context.Users
    .Where(x => x.RoleId == 2)
    .ToList();
                return View(task);
            }

            task.Status = "Not Started";
            task.IsAchived = false;

            _context.TaskItems.Add(task);
            _context.SaveChanges();

            TempData["Success"] = "Task created successfully";

            return RedirectToAction("TasksList");
        }

        public IActionResult TasksList(int? id, string employeeName)
        {
            UpdateTaskStatus();

            var filteredTasks = _context.TaskItems.AsEnumerable();

            if (id != null)
            {
                filteredTasks = filteredTasks.Where(t => t.Id == id);
            }

            if (!string.IsNullOrEmpty(employeeName))
            {
                filteredTasks = filteredTasks.Where(t =>
                    t.AssignedToUserName != null &&
                    t.AssignedToUserName.Trim().ToLower()
                    .Contains(employeeName.Trim().ToLower()));
            }

            return View(filteredTasks.ToList());
        }

        public IActionResult Delete(int id)
        {
            var task = _context.TaskItems.FirstOrDefault(t => t.Id == id);

            if (task != null)
            {
                _context.TaskItems.Remove(task);
                _context.SaveChanges();
            }

            return RedirectToAction("TasksList");
        }

        public IActionResult Edit(int id)
        {
            var task = _context.TaskItems.FirstOrDefault(t => t.Id == id);

            ViewBag.Users = _context.Users
    .Where(x => x.RoleId == 2)
    .Select(x => x.UserName)
    .ToList();

            return View(task);
        }

        [HttpPost]
        public IActionResult Edit(TaskItem updatedTask)
        {
            var task = _context.TaskItems.FirstOrDefault(t => t.Id == updatedTask.Id);

            if (task != null)
            {
                task.Title = updatedTask.Title;
                task.Description = updatedTask.Description;
                task.TargetEntity = updatedTask.TargetEntity;
                task.DueDate = updatedTask.DueDate;
                task.AssignedToUserName = updatedTask.AssignedToUserName;

                _context.SaveChanges();
            }

            return RedirectToAction("TasksList");
        }

        public IActionResult MyTasks()
        {
            return View(_context.TaskItems.ToList());
        }

        public IActionResult Details(int id)
        {
            var task = _context.TaskItems.FirstOrDefault(t => t.Id == id);

            if (task == null)
                return NotFound();

            return View(task);
        }
    }
}