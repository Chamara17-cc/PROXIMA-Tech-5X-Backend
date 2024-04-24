using System;
namespace Project_Management_System.DTOs
{
    public class TaskDescriptionDTO
    {


        public string? TaskName { get; set; }

        public int TaskId { get; set; }

        public string? TaskType { get; set; }

        public string? TaskDescription { get; set; }

        public int TaskPriority { get; set; }

        public string? TaskTechnologies { get; set; }

        public string? TaskComments { get; set; }




        public DateTime TaskStartDate { get; set; }

        public int TaskDuration { get; set; }

        public DateTime TaskDueDate { get; set; }

    }
}

