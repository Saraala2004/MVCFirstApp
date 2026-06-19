using System.ComponentModel.DataAnnotations;//هذا يخلي الفاليديشن يشتقل (الركوايد) يخدم على التحقق في باك ان

namespace MVCFirstApp.Models
{
    public class TaskItem
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]//هذا الكود يمنع الحقل يكون فاضي ويمنع الحفظ  ويعرض رسالة الخطا
        public string Title { get; set; }
        [Required(ErrorMessage = "Description  is required")]
        public string Description { get; set; }
        public string Status { get; set; } = "Not Started";

        [Required(ErrorMessage = " TargetEntity is required")]
        public string TargetEntity { get; set; }
        [Required(ErrorMessage = " AssignedToUserName is required")]
        [StringLength(50)]
        public string AssignedToUserName { get; set; }
        public int AssignedToUserId { get; set; }
        public bool IsAchived { get; set; }
        [Required(ErrorMessage = "DueDate  is required")]

        public DateTime DueDate { get; set; }
    }
}