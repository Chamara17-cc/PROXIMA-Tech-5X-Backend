using System;
namespace Project_Management_System.DTOs
{
    public class ProjectDescriptionDTO
    {

        public string? ProjectName { get; set; }

        public int ProjectId { get; set; }

        public string? ProjectDescription { get; set; }

        public string? ProjectObjectives { get; set; }



        public int ProjectManagerId { get; set; }

        public string? ProjectManagerName { get; set; }



        public DateTime ProjectStartDate { get; set; }

        public int ProjectDuration { get; set; }

        public DateTime ProjectDueDate { get; set; }


    }
}

