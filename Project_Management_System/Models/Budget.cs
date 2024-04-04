using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Project_Management_System.Models
{
    public class Budget
    {
        [Key]
        public int BudgetId { get; set; }
        public string Objectives { get; set; } = string.Empty;
        public double SelectionprocessCost { get; set; }
        public double LicenseCost { get; set; }
        public double ServersCost { get; set; }
        public double HardwareCost { get; set; }
        public double ConnectionCost { get; set; }
        public double DeveloperCost { get; set; }
        public double OtherExpenses { get; set; }
        public double TotalCost { get; set; }



        [JsonIgnore]
        public Project Project { get; set; }
        public int ProjectId { get; set; }
        public ICollection<ViewReport> ViewReports { get; set; }
    }
}
