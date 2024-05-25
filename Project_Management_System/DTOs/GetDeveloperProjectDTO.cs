using System;
namespace Project_Management_System.DTOs
{
    public class GetDeveloperProjectDTO
    {
        public int ProjectId { get; set; }

        public string? ProjectName { get; set; }

        public string? ProjectStatus { get; set; }
    }
}

