using Project_Management_System.Models;

namespace Project_Management_System.DTOs
{
    public class DashboardProjectDto
    {
        public int ProjectId { get; set; }
        public string projectName { get; set; }
        public string projectDescription { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string Technologies { get; set; }
        public int? Total {  get; set; }
        public int Paid {  get; set; }
        public int TotalTask {  get; set; }
        public int CompletedTask {  get; set; }
        public int MonthlyPayment { get; set; }
        public string Status { get; set; }

    }
}
