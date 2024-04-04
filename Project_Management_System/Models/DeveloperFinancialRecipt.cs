using System.ComponentModel.DataAnnotations;

namespace Project_Management_System.Models
{
    public class DeveloperFinancialRecipt
    {
        [Key]
        public int ReceiptId { get; set; }
        public int CurrentMonthWorkingHours { get; set; }
        public int PreviousMonthWorkingHours { get; set; }
        public double HourlyRate { get; set; }

        public Developer Developer { get; set; }
        public int DeveloperId { get; set; }
    }
}
