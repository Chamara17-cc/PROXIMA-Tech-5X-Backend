using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Management_System.Data;
using System.Runtime.InteropServices;

namespace Project_Management_System.Controllers.ClientSideController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public DashboardController(DataContext dataContext) { 
        _dataContext = dataContext;
        }

        [HttpGet("GetProjectsDetails")]
        public async Task<ActionResult> GetProjectsDetails(int clientId)
        {
            var myProjects = await _dataContext.Projects
                .Where(e => e.ClientId == clientId)
                .ToListAsync();

            var projects = new List<DashboardProjectDto>();

            foreach (var project in myProjects)
            {
                int paid = await _dataContext.ClientPayments
                    .Where(e => e.ProjectId == project.ProjectId && (e.Mode == "accepted" || e.Mode == "accept"))
                    .SumAsync(e => e.Payment);


                var projectDto = new DashboardProjectDto
                {
                    ProjectId = project.ProjectId,
                    projectName = project.ProjectName,
                    projectDescription = project.ProjectDescription,
                    Paid = paid,
                    EndDate = DateOnly.FromDateTime(project.P_DueDate),
                    StartDate = DateOnly.FromDateTime(project.P_StartDate),
                    Technologies = project.Technologies,
                    Total = project.InitialBudeget // Use null conditional operator to handle null case
                };

                projects.Add(projectDto);
            }

            return Ok(projects);
        }

    }
}
