using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Management_System.Data;
using Project_Management_System.DTOs;
using Project_Management_System.Models;

namespace Project_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddDevelopersController : ControllerBase
    {
        public readonly DataContext _context;

        public AddDevelopersController(DataContext _context)
        {
            this._context = _context;
        }

        public class DeveloperListDTO
        {
            public string UserId { get; set; }
            public string UserName { get; set; }
            public int JobRoleId { get; set; }

        }

        public class DeveloperProjectDTO
        {
            public int ProjectId { get; set; }
            public int DeveloperId { get; set; }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Developer>>> GetDeveloperList()
        {
            var developers = await _context.Users
                .Where(e => e.JobCategoryId == 3)
                .Select(e => new DeveloperListDTO
                {
                    UserId = e.UserId,
                    UserName = e.UserName,
                    JobRoleId = e.JobRoleId
                })
                .ToListAsync();

            if (developers == null)
            {
                return BadRequest();
            }

            return Ok(developers);

        }



        [HttpPost]      //  https://localhost:44319/api/CreateProject
        public async Task<ActionResult<List<Project>>> Create(ProjectCreationDto request)
        {
            int loggedAdminId = 1;

            var client = await _context.Clients.FindAsync(request.ClientId);
            if (client == null)
            {
                return NotFound();
            }

            var pManager = await _context.ProjectManagers.FindAsync(request.ProjectManagerId);
            if (pManager == null)
            {
                return NotFound();
            }



            var newProject = new Project
            {
                //ProjectId = request.ProjectId,
                ProjectName = request.ProjectName,
                ProjectDescription = request.ProjectDescription,
                Technologies = request.Technologies,
                BudgetEstimation = request.BudgetEstimation,
                P_StartDate = request.P_StartDate,
                P_DueDate = request.P_DueDate,
                Duration = request.Duration,
                TeamName = request.TeamName,
                TimeLine = request.TimeLine,
                Objectives = request.Objectives,
                Client = client,
                ProjectManager = pManager,
                AdminId = loggedAdminId
            };

            _context.Add(newProject);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}

