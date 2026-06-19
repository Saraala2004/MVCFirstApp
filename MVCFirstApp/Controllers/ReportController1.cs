using Microsoft.AspNetCore.Mvc;
using MVCFirstApp.Models;
using MVCFirstApp.Models;

namespace WebDevelopment.Controllers
{
    public class ReportController : Controller
    {
        static List<TaskItem> tasks = new List<TaskItem>();
        private static List<Report> _reports = new List<Report>();
        public IActionResult Create()
        {

            return View(new Report { CreatedDate = DateTime.Now });
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

    }
}