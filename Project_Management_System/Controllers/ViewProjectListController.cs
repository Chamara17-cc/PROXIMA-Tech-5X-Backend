using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Management_System.Data;

namespace Project_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewProjectListController : ControllerBase
    {
        public readonly DataContext _context;

        public ViewProjectListController(DataContext _context)
        {
            this._context = _context;
        }

        public class ViewProjctListDTO
        {
            public int proId { get; set; }
            public string projectName { get; set; }

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewProjctListDTO>>> GetList()
        {
            var projects = await _context.Projects
                .Select(e => new ViewProjctListDTO
                {
                    proId = e.ProjectId,
                    projectName = e.ProjectName
                })
                .ToListAsync();
            return projects;
        }


    }
}
