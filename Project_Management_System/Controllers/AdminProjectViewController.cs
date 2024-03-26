using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Management_System.Data;
using Project_Management_System.DTOs;

namespace Project_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminProjectViewController : ControllerBase
    {
        public readonly DataContext _context;

        public AdminProjectViewController(DataContext _context)
        {
            this._context = _context;
        }


        [HttpGet("{para}")]
        public async Task<ActionResult<IEnumerable<AdminViewDto>>> GetAdminView(int para)
        {


            // int SelectedProId = c1;

            var SelectedProject = await _context.Projects
                .Where(e => e.ProjectId == para)
                .Select(e => new AdminViewDto
                {
                    ProjectId = e.ProjectId,
                    ProjectName = e.ProjectName,
                    ProjectDescription = e.ProjectDescription,
                    Technologies = e.Technologies,
                    BudgetEstimation = e.BudgetEstimation,
                    P_StartDate = e.P_StartDate,
                    P_DueDate = e.P_DueDate,
                    Duration = e.Duration,
                    TeamName = e.TeamName,
                    TimeLine = e.TimeLine,
                    Objectives = e.Objectives,
                    ClientId = e.ClientId,

                    ProjectManagerId = e.ProjectManagerId
                }).ToListAsync();

            if (SelectedProject == null)
            {
                return BadRequest();
            }


            return SelectedProject;


        }
    }
}
