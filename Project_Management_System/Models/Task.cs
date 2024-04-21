﻿using System.ComponentModel.DataAnnotations;

namespace Project_Management_System.Models
{
    public class Task
    {
        [Key]
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public int TimeDuration { get; set; }
        public string? Technology { get; set; }
        public string? Dependancy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DueDate { get; set; }
        public int Priority { get; set; }
        //     public int UserId { get; set; }

        public Project Project { get; set; }
        public int ProjectId { get; set; }

        public Developer Developer { get; set; }
        public int DeveloperId { get; set; }

        public List<FileResource> FileResources { get; set; }


    }
}
