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
    public class ProjectProgressReportController : ControllerBase
    {


        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ProjectProgressReportController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }



        [HttpGet("ProjectProgressReport/{projectid}")]

        public async Task<ActionResult<IEnumerable<Project>>> getDeveloperProjectProgress(int projectid)
        {

            var projects = await _context.Projects
                .Where(e => e.ProjectId == projectid)
                .Select(e => new GetProjectProgressDetailsDTO
                {
                    TotalProjectRemainingHours = e.TotalProjectRemainingHours,
                    TotalProjectCompletedHours = e.TotalProjectCompletedHours

                }).ToListAsync();

            if (projects == null)
            {
                Console.WriteLine("Projects are not added");
            }


            return Ok(projects);

        }






    }
}

