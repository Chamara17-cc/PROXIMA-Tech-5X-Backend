using System.ComponentModel.DataAnnotations;

namespace Project_Management_System.Models
{
    public class UserRole
    {

        [Key]
        public string RoleId { get; set; }
        [Required]
        public string RoleName { get; set; }

    }
}