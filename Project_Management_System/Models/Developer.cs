using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Management_System.Models
{
    public class Developer
    {
        [ForeignKey("User")]
        public int DeveloperId { get; set; }

        public int FinanceReceiptId { get; set; }

        public virtual User User { get; set; }
        public List<Task> Tasks { get; set; }

        public List<DeveloperProject> DeveloperProjects { get; set; }

    }
}
