using System.ComponentModel.DataAnnotations;

namespace Project_Management_System.Models
{
    public class DeveloperProject
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        [Required]
        public int DeveloperId { get; set; }
        public Developer Developer { get; set; }
    }
}
