using System.ComponentModel.DataAnnotations;

namespace Project_Management_System.Models
{
    public class JobRole
    {

        [Key]
        public int JobRoleId { get; set; }
        [Required]
        public string JobRoleType { get; set; }

    }
}