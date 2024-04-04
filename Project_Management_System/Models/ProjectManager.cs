namespace Project_Management_System.Models
{
    public class ProjectManager
    {
        public int Id { get; set; }

        public int ProjectManagerId { get; set; }
        public List<Project> Projects { get; set; }
    }
}
