global using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Project_Management_System.Data;
using Project_Management_System.DTOs;
using Project_Management_System.Models;

namespace Project_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        private readonly DataContext _budgetdatacontext;
        private readonly IMapper _mapper;


        public BudgetController(DataContext dataContext, IMapper mapper)
        {
            _budgetdatacontext = dataContext;
            _mapper = mapper;
        }
        public static List<Budget> budgetItems = new List<Budget>()
        {
            new Budget(),
        };

        [HttpGet]
        public async Task<ActionResult<List<GetProjectDto>>> GetProject()
        {
            var projectlist = await _budgetdatacontext.Projects.ToListAsync();
            return Ok(projectlist);
        }

        [HttpPost("Projects/{projectid}")]
        public async Task<ActionResult<List<GetBudgetDto>>> AddBudget(AddBudgetDto budgelist, int projectid)
        {
            var project = await _budgetdatacontext.Projects.FindAsync(projectid);
            if (project == null)
            {
                return NotFound("No any projects");
            }
            var budgetItem = _mapper.Map<Budget>(budgelist);
            budgetItem.ProjectId = projectid;
            _budgetdatacontext.Budgets.Add(budgetItem);
            await _budgetdatacontext.SaveChangesAsync();
            var budgetItems = await _budgetdatacontext.Budgets.ToListAsync();
            var addedBudgetDTOs = _mapper.Map<List<GetBudgetDto>>(budgetItems);
            return Ok(addedBudgetDTOs);
        }

        [HttpGet("Projects/{projectid}")]
        public async Task<ActionResult<List<GetBudgetDto>>> GetBudget(int projectid)
        {
            var Projectid = await _budgetdatacontext.Projects.FindAsync(projectid);
            if (Projectid == null)
            {
                throw new Exception("Not found");
            }
            var budgetdata = await _budgetdatacontext.Budgets.Where(b => b.ProjectId == projectid).ToListAsync();
            var getbudgetlist = _mapper.Map<List<GetBudgetDto>>(budgetdata);
            return Ok(getbudgetlist);
        }
        [HttpPut("Projects/{projectid}")]
        public async Task<ActionResult<List<UpdateBudgetDto>>> UpdateBudget(int projectid, [FromBody] UpdateBudgetDto updatebudget)
        {
            try
            {

                var excistingbudget = await _budgetdatacontext.Budgets.FirstOrDefaultAsync(p => p.ProjectId == projectid);
                if (excistingbudget == null)
                {
                    throw new Exception("Budget Not Created");
                }
                excistingbudget.Objectives = updatebudget.Objectives;
                excistingbudget.SelectionprocessCost = updatebudget.SelectionprocessCost;
                excistingbudget.LicenseCost = updatebudget.LicenseCost;
                excistingbudget.ServersCost = updatebudget.ServersCost;
                excistingbudget.HardwareCost = updatebudget.HardwareCost;
                excistingbudget.ConnectionCost = updatebudget.ConnectionCost;
                excistingbudget.DeveloperCost = updatebudget.DeveloperCost;
                excistingbudget.OtherExpenses = updatebudget.OtherExpenses;
                excistingbudget.TotalCost = updatebudget.TotalCost;

                _budgetdatacontext.Budgets.Update(excistingbudget);
                await _budgetdatacontext.SaveChangesAsync();
                return Ok("Updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
    }
}
