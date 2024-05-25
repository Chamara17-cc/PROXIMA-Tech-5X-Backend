using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Management_System.Data;
using Project_Management_System.Models;

namespace Project_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetClientNamesController : ControllerBase
    {
        public readonly DataContext _context;

        public GetClientNamesController(DataContext _context)
        {
            this._context = _context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetCNames()
        {
            var clients = await _context.Clients
                .Select(e => new GetClientNamesDTO
                {
                    ClientId = e.ClientId,
                    ClientName = e.ClientName
                }).ToListAsync();

            if(clients == null)
            {
                return NotFound();
            }

            return Ok(clients);
        }
    }
}
