using System.ComponentModel.DataAnnotations;

namespace MVCFirstApp.Models
{
    public class Report
    {
        public int ReportId { get; set; }

        public int TaskId { get; set; }

        public int UserId { get; set; }

        [Required(ErrorMessage = "Summary field is required and cannot be left blank.")]
        [StringLength(2000, ErrorMessage = "The summary is too long. The maximum allowed length is 2000 characters")]
        public string Summary { get; set; }

        [Required(ErrorMessage = "Employee opinion field is required and cannot be left blank")]
        [StringLength(500, ErrorMessage = "The Employee opinion  is too long. The maximum allowed length is 500 characters")]
        public string EmployeeOpinion { get; set; }

        public string Status { get; set; }

        public string ManagerComment { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsArchived { get; set; } = false;
    }
}