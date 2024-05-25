using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Management_System.Data;

namespace Project_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskCreationController : ControllerBase
    {
        public readonly DataContext _context;

        public TaskCreationController(DataContext _context)
        {
            this._context = _context;
        }


        [HttpPost]
        public async Task<ActionResult<List<Models.Task>>> CreateTask(TaskCreationDTO request)
        {
            var project = await _context.Projects.FindAsync(request.ProjectId);
            if (project == null)
            {
                return NotFound();
            }

            var dev = await _context.Developers.FindAsync(request.DeveloperId);
            if (dev == null)
            {
                return NotFound();
            }

            var newTask = new Models.Task
            {
                //TaskId = request.TaskId,
                TaskName = request.TaskName,
                TaskDescription = request.TaskDescription,
                Technology = request.Technology,
                Dependancy = request.Dependancy,
                Priority = request.Priority,
                CreatedDate = request.CreatedDate,
                DueDate = request.DueDate,
                TimeDuration = request.TimeDuration,
                TaskStatus = 0,
                Project = project,
                Developer = dev


            };




            _context.Add(newTask);
            await _context.SaveChangesAsync();
            return Ok();


        }
    }
}
