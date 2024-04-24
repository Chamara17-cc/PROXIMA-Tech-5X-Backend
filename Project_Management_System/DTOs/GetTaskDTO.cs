using System;
namespace Project_Management_System.DTOs
{
    public class GetTaskDTO
    {
        public int TaskId { get; set; }

        public string? TaskName { get; set; }

        public DateTime DueDate { get; set; }

        public int? TaskStatus { get; set; }
    }
}

