using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Project_Management_System.Data;
using Project_Management_System.DTOs;
using Project_Management_System.Models;


namespace Pro.Controllers
{
    [Route("api/[controller]")]
    public class DeveloperTaskController : ControllerBase
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DeveloperTaskController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }



        // GET: api/values
        [HttpGet]
        [Route("GetAllTasks/{para}")]
        public async Task<ActionResult<IEnumerable<GetTaskDTO>>> GetDeveloperTasks(int para)
        {
            var tasks = await _context.Tasks.
                Where(e => e.DeveloperId == para)
                .Select(e => new GetTaskDTO
                {
                    TaskId = e.TaskId,
                    TaskName = e.TaskName,
                    DueDate = e.DueDate,
                    TaskStatus = e.TaskStatus
                }).ToListAsync();

            if (tasks == null)
            {
                throw new Exception("Not Found");
            }

            //return var projects to ProjectDTO class
            return _mapper.Map<List<GetTaskDTO>>(tasks);
        }



        [HttpGet("{id}")]
        public async Task<TaskDescriptionDTO> GetTaskDetails(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                throw new Exception("Not Found");
            }

            return _mapper.Map<TaskDescriptionDTO>(task);
        }



    }
}

