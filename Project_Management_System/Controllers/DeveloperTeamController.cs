using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project_Management_System.Data;
using Project_Management_System.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project_Management_System.Controllers
{
    [Route("api/[controller]")]
    public class DeveloperTeamController : ControllerBase
    {


        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DeveloperTeamController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("GetAllTeams/{para}")]
        public async Task<ActionResult<IEnumerable<Project>>> getTeams(int para)
        {

            var teams = await _context.DeveloperProjects
                .Where(e => e.DeveloperId == para)
                .Select(e => new GetTeamDTO
                {
                    ProjectId = e.ProjectId,
                    TeamName = e.Project.TeamName,
                    ProjectStatus = e.Project.ProjectStatus

                }).ToListAsync();

            if (teams == null)
            {
                Console.WriteLine("Projects are not added");
            }


            return Ok(teams);

        }









    }
}

