using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Management_System.Models
{
    public class ProjectManager
    {
        [ForeignKey("User")]
        public int ProjectManagerId { get; set; }

        public virtual User User { get; set; }
        public List<Project> Projects { get; set; }

    }
}
