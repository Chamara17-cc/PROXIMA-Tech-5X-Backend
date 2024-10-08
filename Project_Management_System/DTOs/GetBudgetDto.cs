﻿namespace Project_Management_System.DTOs
{
    public class GetBudgetDto
    {
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

        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime Date { get; set; }
    }
}
