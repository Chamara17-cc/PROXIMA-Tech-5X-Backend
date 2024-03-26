using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Project_Management_System.Models
{
    public class Users : IdentityUser
    {

        /*[Key]
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
        public string Useremail { get; set; }

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

        public string jobCategoryId { get; set; }*/

        [Key]
        public string UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string PasswordHash { get; set; }

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

        public string ProfilePictureLink { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime LastLoginDate { get; set; }

        public bool IsActive { get; set; } = true;

        public string JobRoleId { get; set; }

        public string JobCategoryId { get; set; }
    }
}
