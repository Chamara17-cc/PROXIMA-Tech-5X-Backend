﻿namespace Project_Management_System.DTOs
{
    public class TaskCreationDTO
    {
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public string? Technology { get; set; }
        public string? Dependancy { get; set; }
        public int Priority { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DueDate { get; set; }
        public int TimeDuration { get; set; }
        public int ProjectId { get; set; }
        public int DeveloperId { get; set; }
        

    }
}
