using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Pro.Context;
using Pro.Models.DTOs;
using Pro.Models;


namespace Pro.Controllers
{
    [Route("api/[controller]")]
    public class DeveloperProjectController : ControllerBase
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DeveloperProjectController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }



        // GET: api/values
        [HttpGet]
        [Route("GetAllProject")]
        public async Task<IEnumerable<GetProjectDTO>>? GetAll()
        {
            var projects = await _context.Projects.ToListAsync();

            if (projects == null)
            {
                throw new Exception("Not Found");
            }

            //return var projects to ProjectDTO class
            return _mapper.Map<List<GetProjectDTO>>(projects);
        }




        [HttpGet("{id}")]
        public async Task<ProjectDescriptionDTO> GetProjectDetails(int id)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project == null)
            {
                throw new Exception("Not Found");
            }

            return _mapper.Map<ProjectDescriptionDTO>(project);
        }



















    }

}
