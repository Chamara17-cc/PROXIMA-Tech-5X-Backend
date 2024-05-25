using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Project_Management_System.Data;
using Project_Management_System.DTOs;
using Project_Management_System.Models;


namespace Project_Management_System.Controllers
{
    [Route("api/[controller]")]
    public class DeveloperProjectController : ControllerBase
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DeveloperProjectController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet("{para}")]
        public async Task<ActionResult<IEnumerable<Project>>> getDeveloperProjects(int para)
        {

            var projects = await _context.DeveloperProjects
                .Where(e => e.DeveloperId == para)
                .Select(e => new GetDeveloperProjectDTO
                {
                    ProjectId = e.ProjectId,
                    ProjectName = e.Project.ProjectName,
                    ProjectStatus = e.Project.ProjectStatus

                }).ToListAsync();

            if (projects == null)
            {
                Console.WriteLine("Projects are not added");
            }


            return Ok(projects);

        }






        [HttpGet("ProjectDescription/{id}")]
        public async Task<ProjectDescriptionDTO> GetProjectDetails(int id)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project == null)
            {
                throw new Exception("Not Found");
            }

            return _mapper.Map<ProjectDescriptionDTO>(project);
        }





    }

}
