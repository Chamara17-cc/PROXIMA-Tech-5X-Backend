namespace Project_Management_System.Models
{
    public class Developer
    {
        public int Id { get; set; }
        public int DeveloperId { get; set; }
        //public User User { get; set; }
        public int FinanceReceiptId { get; set; }

        public List<Task> Tasks { get; set; }


        public List<DeveloperProject> DeveloperProjects { get; set; }

        public DeveloperFinancialRecipt DeveloperFinancialRecipt { get; set; }
    }
}
