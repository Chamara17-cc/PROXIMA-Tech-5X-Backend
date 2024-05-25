using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Management_System.Data;
using System.Security.Cryptography;

namespace Project_Management_System.Controllers.TaskControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskFileDeleteController : ControllerBase
    {
        public readonly DataContext _context;

        public TaskFileDeleteController(DataContext _context)
        {
            this._context = _context;            
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFiles(int id)
        {
            try
            {
                var files = await _context.FileResources
                    .Where(e => e.TaskId == id)
                    .ToListAsync();
                
                for(int i = 0; i < files.Count; i++)
                {
                    var file = files[i];
                    _context.FileResources.Remove(file);
                    await _context.SaveChangesAsync();
                }

                return NoContent();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
