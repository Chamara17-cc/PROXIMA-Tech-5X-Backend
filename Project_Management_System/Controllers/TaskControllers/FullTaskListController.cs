﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Management_System.Data;
using Project_Management_System.Models;
using static Project_Management_System.Controllers.TaskControllers.TaskListController;

namespace Project_Management_System.Controllers.TaskControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FullTaskListController : ControllerBase
    {
        public readonly DataContext _context;

        public FullTaskListController(DataContext _context)
        {
            this._context = _context;
        }

        public class FullTaskListDTO
        {
            public int TaskId { get; set; }
            public string TaskName { get; set; }
            public string DeveloperFName { get; set; }
            public string DeveloperLName { get; set; }
            public int TaskStatus { get; set; }

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Task>>> GetTasks(int ProId)
        {

            var tasks = await _context.Tasks
                .Where(x => x.ProjectId == ProId)
                .Select(x => new FullTaskListDTO
                {
                    TaskId = x.TaskId,
                    TaskName = x.TaskName,
                    TaskStatus = x.TaskStatus,
                    DeveloperFName = x.Developer.User.FirstName,
                    DeveloperLName = x.Developer.User.LastName

                }).ToListAsync();

            return Ok(tasks);
        }
    }
}
