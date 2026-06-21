using Microsoft.AspNetCore.Mvc;
using MVCFirstApp.Models;

namespace WebDevelopment.Controllers
{
    public class ReportController : Controller
    {
        static List<TaskItem> tasks = new List<TaskItem>();
        private static List<Report> _reports = new List<Report>();
        public IActionResult Create(int taskId)
        {
            var report = new Report
            {
                TaskId = taskId,
                CreatedDate = DateTime.Now
            };
            return View(report);
        }
        [HttpPost]

        public IActionResult Create(Report report)
        {
           

            report.ReportId = _reports.Count + 1;
            report.Status = "Pending Review";
            report.CreatedDate = DateTime.Now;

            _reports.Add(report);

            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            ViewBag.UserRole = "Manager";


            return View(_reports);
        }
        // EDIT GET
        public IActionResult Edit(int id)
        {
            var report = _reports.FirstOrDefault(r => r.ReportId == id);
            if (report == null) return NotFound();

            return View(report);
        }

        // EDIT POST
        [HttpPost]
        public IActionResult Edit(Report updated)
        {
            var report = _reports.FirstOrDefault(r => r.ReportId == updated.ReportId);
            if (report == null) return NotFound();

            report.Summary = updated.Summary;
            report.EmployeeOpinion = updated.EmployeeOpinion;
            report.Status = updated.Status;

            return RedirectToAction("Index");
        }

        // DELETE
        public IActionResult Delete(int id)
        {
            var report = _reports.FirstOrDefault(r => r.ReportId == id);

            if (report != null)
                _reports.Remove(report);

            return RedirectToAction("Index");
        }

        // APPROVE / REJECT (Manager)
        public IActionResult Approve(int id)
        {
            var report = _reports.FirstOrDefault(r => r.ReportId == id);
            if (report == null) return NotFound();

            report.Status = "Approved";
            report.IsArchived = true;

            return RedirectToAction("Index");
        }

        public IActionResult Reject(int id, string comment)
        {
            var report = _reports.FirstOrDefault(r => r.ReportId == id);
            if (report == null) return NotFound();

            report.Status = "Needs Revision";
            report.ManagerComment = comment;

            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            var report = _reports.FirstOrDefault(r => r.ReportId == id);
            if (report == null)
            {
                return NotFound();
            }
            return View(report);
        }
        [HttpPost]
        public IActionResult AddComment(int ReportId, string Comment)//بحث يستخدم لان بعد مدير يكتب تعليق سيرفر مش حيعرف لمن تعليق هذا فهو حيتعامل بالرقم عشان يوصل فالريبورت
        {
            
            var report = _reports.FirstOrDefault(r => r.ReportId == ReportId);

           
            if (report == null)
            {
                return NotFound();
            }

            
            report.ManagerComment = Comment;
            report.Status = "Revision Required"; // تم تغيير الحالة تلقائياً ليفهم الموظف أنه مطلوب منه تعديل

            // 3️⃣ حفظ التغييرات (إذا كنتِ تستخدمين قاعدة بيانات Entity Framework فكّي التعليق عن السطر التالي)
            // _context.SaveChanges();

            
            return RedirectToAction("Index");

        }

    }
}