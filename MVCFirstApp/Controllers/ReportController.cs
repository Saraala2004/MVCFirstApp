using Microsoft.AspNetCore.Mvc;
using MVCFirstApp.Models;
using MVCFirstApp.Data;

namespace MVCFirstApp.Controllers
{
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }


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
            if (!ModelState.IsValid)
            {
                return View(report);
            }

            report.Status = "Pending Review";
            report.CreatedDate = DateTime.Now;
            report.IsArchived = false;

            _context.Reports.Add(report);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult Index()
        {
            ViewBag.UserRole = "Employee";

            var reports = _context.Reports.ToList();

            return View(reports);
        }


        public IActionResult Edit(int id)
        {
            var report = _context.Reports
                .FirstOrDefault(r => r.ReportId == id);

            if (report == null)
                return NotFound();

            return View(report);
        }


        [HttpPost]
        public IActionResult Edit(Report updated)
        {
            var report = _context.Reports
                .FirstOrDefault(r => r.ReportId == updated.ReportId);

            if (report == null)
                return NotFound();

            report.Summary = updated.Summary;
            report.EmployeeOpinion = updated.EmployeeOpinion;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            var report = _context.Reports
                .FirstOrDefault(r => r.ReportId == id);

            if (report != null)
            {
                _context.Reports.Remove(report);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        public IActionResult Approve(int id)
        {
            var report = _context.Reports
                .FirstOrDefault(r => r.ReportId == id);

            if (report == null)
                return NotFound();

            report.Status = "Approved";
            report.IsArchived = true;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult Reject(int id, string comment)
        {
            var report = _context.Reports
                .FirstOrDefault(r => r.ReportId == id);

            if (report == null)
                return NotFound();

            report.Status = "Needs Revision";
            report.ManagerComment = comment;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult Details(int id)
        {
            var report = _context.Reports
                .FirstOrDefault(r => r.ReportId == id);

            if (report == null)
                return NotFound();

            return View(report);
        }


        [HttpPost]
        public IActionResult AddComment(int ReportId, string Comment)
        {
            var report = _context.Reports
                .FirstOrDefault(r => r.ReportId == ReportId);

            if (report == null)
                return NotFound();

            report.ManagerComment = Comment;
            report.Status = "Revision Required";

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}