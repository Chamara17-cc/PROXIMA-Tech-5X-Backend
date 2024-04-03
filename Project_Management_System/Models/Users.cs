using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Project_Management_System.Models
{
    public class Users 
    {
        internal string JobRoleName;

        /*[Key]
        public string UserId { get; set; }
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Useremail { get; set; }


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

        public int JobRoleId { get; set; }

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

           
        public int JobCategoryId { get; set; }


        public int UserCategoryId { get; set; }
        public UserCategory UserCategories { get; set; }

        public int UserRoleId { get; set; } 
        public UserRole UserRoles { get; set;}

        public List<ViewResource> ViewResources { get; set; }
    }
}
