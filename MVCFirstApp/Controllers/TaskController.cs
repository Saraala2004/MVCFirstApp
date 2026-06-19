using Microsoft.AspNetCore.Mvc;
using MVCFirstApp.Models;

namespace MVCFirstApp.Controllers
{
    public class TaskController : Controller
    {
        static List<TaskItem> tasks = new List<TaskItem>();
        public IActionResult Index()
        {
            ViewBag.NotStartedTasks = tasks.Count(t => t.Status == "Not Started");

            ViewBag.CompletedTasks = tasks.Count(t => t.Status == "Completed");

            ViewBag.InProgressTasks = tasks.Count(t => t.Status == "In Progress");

            ViewBag.DelayedTasks = tasks.Count(t => t.Status == "Delayed");

            UpdateTaskStatus();


            return View(tasks);

        }
        private void UpdateTaskStatus()//دالة اللي تعدل التاسك حالتها في فال فات موعد الديو ديت وهي ماتنفذتش 
        {
            foreach (var task in tasks)
            {
                if (task.DueDate.Date < DateTime.Now.Date && task.Status != "Completed")
                {
                    task.Status = "Delayed";
                }
            }
        }

        public IActionResult Create()
        {//Get 
            var newtask = new TaskItem();

            newtask.DueDate = DateTime.Now;

            return View(newtask);
        }
        [HttpPost]
        public IActionResult Create(TaskItem task)  // POST
        {
            var newtask = new TaskItem();
            newtask.Id = tasks.Count + 1;
            newtask.Title = task.Title;
            newtask.DueDate = task.DueDate;
            newtask.TargetEntity = task.TargetEntity;
            newtask.Description = task.Description;
            newtask.AssignedToUserName = task.AssignedToUserName;
            tasks.Add(newtask);
            TempData["Success"] = "Task created successfully";
            return RedirectToAction("TasksList");
        }

        public IActionResult TasksList(int? id, string employeeName)
        {

            var filteredTasks = tasks.AsEnumerable();

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
            UpdateTaskStatus();
            return View(filteredTasks.ToList());
        }
        public IActionResult Delete(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);

            if (task != null)
            {
                tasks.Remove(task);
            }

            return RedirectToAction("TasksList");
        }
        public IActionResult Edit(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            ViewBag.Users = new List<string>
    {
        "Ahmed",
        "Ali",
        "Asma",
        "Mohammed"
    };

            return View(task);
        }
        [HttpPost]
        public IActionResult Edit(TaskItem updatedTask)
        {
            var task = tasks.FirstOrDefault(t => t.Id == updatedTask.Id);

            if (task != null)
            {
                task.Title = updatedTask.Title;
                task.Description = updatedTask.Description;
                task.TargetEntity = updatedTask.TargetEntity;
                task.DueDate = updatedTask.DueDate;
                task.AssignedToUserName = updatedTask.AssignedToUserName;
            }

            return RedirectToAction("TasksList");
        }
        public IActionResult MyTasks()
        {
            return View(tasks);
        }
        public IActionResult Details(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
                return NotFound();

            return View(task);
        }
    }

}