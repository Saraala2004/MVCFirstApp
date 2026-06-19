namespace MVCFirstApp.Models

{

    public class Report
    {
        public int ReportId { get; set; }

        public int TaskId { get; set; }

        public int UserId { get; set; }

        public string Summary { get; set; }

        public string EmployeeOpinion { get; set; }

        public string Status { get; set; }

        public string ManagerComment { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsArchived { get; set; }
    }
}