using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Management_System.Data;
using Project_Management_System.Models;

namespace Project_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckAddedDevelopersController : ControllerBase
    {
        public readonly DataContext _context;

        public CheckAddedDevelopersController(DataContext _context)
        {
            this._context = _context;

        }

        [HttpGet("{para}")]
        public async Task<ActionResult<IEnumerable<DeveloperProject>>> CheckDev(int para)
        {
            var addedDev = await _context.DeveloperProjects
                .Where(x => x.ProjectId == para)
                .Select(x => new DeveloperProjectDTO
                {
                    DeveloperId = x.DeveloperId,
                    ProjectId = x.ProjectId
                }).ToListAsync();

            return Ok(addedDev);
        }

    }
}
