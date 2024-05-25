using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Management_System.Data;
using Project_Management_System.Models;

namespace Project_Management_System.Controllers.PManagerControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PmProjectListController : ControllerBase
    {
        public readonly DataContext _context;

        public PmProjectListController(DataContext _context)
        {
            this._context = _context;
        }

        public class ViewProjctListDTO
        {
            public int proId { get; set; }
            public string projectName { get; set; }
            public string projectStatus {  get; set; }


        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetList(int id)
        {
            try
            {
                var projects = await _context.Projects
                .Where(e => e.ProjectManagerId == id)
                .Select(e => new ViewProjctListDTO
                {
                    proId = e.ProjectId,
                    projectName = e.ProjectName,
                    projectStatus = e.ProjectStatus

                }).ToListAsync();

                return Ok(projects);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
