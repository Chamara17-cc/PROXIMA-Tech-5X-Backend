using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Management_System.Models
{
    public class Admin
    {
        [ForeignKey("User")]
        public int AdminId { get; set; }

        public virtual User User { get; set; }
        public List<Project> Projects { get; set; }

    }
}
