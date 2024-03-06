using System.ComponentModel.DataAnnotations;

namespace Project_Management_System.Models
{
    public class Users : IdentityUser
    {

        [Key]
        public string UserId { get; set; }
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string passwordHash { get; set; } = string.Empty;
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public string email { get; set; }

        public string contactNumber { get; set; }

        public string address { get; set; }
        [Required]
        public string NIC { get; set; }

        public DateTime DOB { get; set; }

        public string gender { get; set; }

        public string profilePictureLink { get; set; }

        public DateTime createdAt { get; set; } = DateTime.Now;

        public DateTime lastLoginDate { get; set; }

        public bool isActive { get; set; } = true;

        public string jobRoleId { get; set; }

        public string jobCategoryId { get; set; }
    }
}
