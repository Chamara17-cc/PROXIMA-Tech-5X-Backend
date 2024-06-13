using System;
namespace Project_Management_System.DTOs
{
    public class GetProjectProgressDetailsDTO
    {
        public int TotalProjectCompletedHours { get; set; }
        public int TotalProjectRemainingHours { get; set; }
    }
}

