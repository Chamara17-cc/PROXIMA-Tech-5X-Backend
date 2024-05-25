using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Management_System.Data;

namespace Project_Management_System.Controllers.TaskControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskListController : ControllerBase
    {
        public readonly DataContext _context;

        public TaskListController(DataContext _context)
        {
            this._context = _context;
        }

        public class TaskListDTO
        {
            public int TaskId { get; set; }
            public string TaskName { get; set; }
            public int TaskStatus { get; set; }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Task>>> GetTaskList(int ProId, int DevId)
        {
            var tasks = await _context.Tasks
                .Where(x => x.ProjectId == ProId && x.Developer.DeveloperId == DevId)
                .Select(x => new TaskListDTO
                {
                    TaskId = x.TaskId,
                    TaskName = x.TaskName,
                    TaskStatus = x.TaskStatus
                }).ToListAsync();

            return Ok(tasks);
        }
    }
}
