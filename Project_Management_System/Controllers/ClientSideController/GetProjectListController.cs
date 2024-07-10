using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Management_System.Data;
using Project_Management_System.Models;

namespace Project_Management_System.Controllers.ClientSideControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetProjectListController : ControllerBase
    {
        public readonly DataContext _context;

        public GetProjectListController(DataContext _context)
        {
            this._context = _context;
        }

        public class ClientProjectListDTO
        {
            public int ProjectId { get; set; }
            public string ProjectName { get; set; }
            public string ProjectStatus { get; set; }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects(int id)
        {
            var projects = await _context.Projects
                .Where(c => c.ClientId == id)
                .Select(c => new ClientProjectListDTO
                {
                    ProjectId = c.ProjectId,
                    ProjectName = c.ProjectName,
                    ProjectStatus = c.ProjectStatus

                }).ToListAsync();

            if (projects == null)
            {
                return NotFound();
            }

            return Ok(projects);

        }
    }
}