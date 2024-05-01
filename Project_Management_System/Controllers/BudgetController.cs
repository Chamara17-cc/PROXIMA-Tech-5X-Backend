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
            var projectDtos= _mapper.Map<List<GetProjectDto>>(projectlist);
            return Ok(projectDtos);
        }

        [HttpPost("Projects/{projectid}")]
        public async Task<ActionResult<List<Budget>>> AddBudget(AddBudgetDto budgelist, int projectid)
        {
            try
            {
                var project = await _budgetdatacontext.Projects.FindAsync(projectid);
                if (project == null)
                {
                    return NotFound("No projects found with the provided project ID");
                }

                var budgetItem = _mapper.Map<Budget>(budgelist);
                budgetItem.ProjectId = projectid;

                _budgetdatacontext.Budgets.Add(budgetItem);
                await _budgetdatacontext.SaveChangesAsync();

                var budgetItems = await _budgetdatacontext.Budgets.ToListAsync();
                var addedBudgetDTOs = _mapper.Map<List<GetBudgetDto>>(budgetItems);

                return Ok(addedBudgetDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }


        [HttpGet("Projects/{projectid}")]
        public async Task<ActionResult<List<GetBudgetDto>>> GetBudget(int projectid)
        {
            try
            {
                var Projectid = await _budgetdatacontext.Projects.FindAsync(projectid);
                if (Projectid == null)
                {
                    return NotFound();
                }
                var budgetdata = await _budgetdatacontext.Budgets.Where(b => b.ProjectId == projectid).ToListAsync();
                var getbudgetlist = _mapper.Map<List<GetBudgetDto>>(budgetdata);
                return Ok(getbudgetlist);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }
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

                if (updatebudget.Objectives != null)
                    excistingbudget.Objectives = updatebudget.Objectives;

                if (updatebudget.SelectionprocessCost != 0)
                    excistingbudget.SelectionprocessCost = updatebudget.SelectionprocessCost;

                if (updatebudget.LicenseCost != 0)
                    excistingbudget.LicenseCost = updatebudget.LicenseCost;

                if (updatebudget.ServersCost != 0)
                    excistingbudget.ServersCost = updatebudget.ServersCost;

                if (updatebudget.HardwareCost != 0)
                    excistingbudget.HardwareCost = updatebudget.HardwareCost;

                if (updatebudget.ConnectionCost != 0)
                    excistingbudget.ConnectionCost = updatebudget.ConnectionCost;

                if (updatebudget.DeveloperCost != 0)
                    excistingbudget.DeveloperCost = updatebudget.DeveloperCost;

                if (updatebudget.OtherExpenses != 0)
                    excistingbudget.OtherExpenses = updatebudget.OtherExpenses;

                if (updatebudget.TotalCost != 0)
                    excistingbudget.TotalCost = updatebudget.TotalCost;

                _budgetdatacontext.Budgets.Update(excistingbudget);
                await _budgetdatacontext.SaveChangesAsync();
                return Ok("Updated");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }


    }
}
