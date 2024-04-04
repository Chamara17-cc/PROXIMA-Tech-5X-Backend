﻿using System.ComponentModel.DataAnnotations;

namespace Project_Management_System.Models
{
    public class ViewReport
    {

        [Key]
        public int BudgetId { get; set; }

        public int UserId { get; set; }

        public Budget Budgets { get; set; }
        public Admin Admins { get; set; }
    }
}
