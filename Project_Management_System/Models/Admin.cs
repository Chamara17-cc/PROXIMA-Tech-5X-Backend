namespace Project_Management_System.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public int AdminId { get; set; }
        //public User User { get; set; }


        public List<Project> Projects { get; set; }

        public ICollection<ViewInvoice> ViewInvoices { get; set; }
        public ICollection<ViewReport> ViewReports { get; set; }
    }
}
