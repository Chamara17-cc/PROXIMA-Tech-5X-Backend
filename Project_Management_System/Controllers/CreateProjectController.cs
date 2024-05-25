using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Management_System.Data;
using Project_Management_System.Models;

namespace Project_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateProjectController : ControllerBase
    {
        public readonly DataContext _context;

        public CreateProjectController(DataContext _context)
        {
            this._context = _context;
        }

        [HttpPost]      //  https://localhost:44339/api/CreateProject
        public async Task<ActionResult<List<Project>>> Create(ProjectDto request)
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

                ProjectStatus = "New",

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
