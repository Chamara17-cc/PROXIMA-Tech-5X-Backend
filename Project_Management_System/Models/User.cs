using System.ComponentModel.DataAnnotations;

namespace Project_Management_System.Models
{
    public class User
    {

        [Key]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        [Required]
        public string NIC { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
       // public string ProfilePictureLink { get; set; }
        public DateTime JoinDate { get; set; } = DateTime.Now;
        public DateTime LastLoginDate { get; set; } 
        public bool IsActive { get; set; } = true;
        public int JobRoleId { get; set; }
        public JobRole JobRole { get; set; }

        public int UserCategoryId { get; set; }
        public UserCategory UserCategory { get; set; }


        public Developer Developer { get; set; }
        public Admin Admin { get; set; }
        public ProjectManager ProjectManager { get; set; }


    }
}
