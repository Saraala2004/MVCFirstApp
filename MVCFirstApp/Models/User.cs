using System.ComponentModel.DataAnnotations;

namespace MVCFirstApp.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "ادخل كلمة المرور")]
        [MinLength(8, ErrorMessage = "الباسورد لازم يكون 8 أرقام أو أكثر")]

        public string Password { get; set; }
        


        [Range(1, 3, ErrorMessage = "Role Id must be between 1 and 3")]
        public int RoleId { get; set; }
    }
}