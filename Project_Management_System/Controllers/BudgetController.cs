using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly DataContext _projectdatacontext;

        public BudgetController(DataContext dataContext, IMapper mapper)
        {
            _budgetdatacontext = dataContext;
            _mapper = mapper;
            _projectdatacontext = dataContext;
        }
        public static List<Budget> budgetItems = new List<Budget>()
        {
            new Budget(),
        };

        [HttpGet]
        public async Task<ActionResult<List<GetProjectNameDTO>>> GetProject()
        {
            var projectlist = await _projectdatacontext.Projects.ToListAsync();
            return Ok(projectlist);
        }

        [HttpPost]
        public async Task<ActionResult<List<GetBudgetDTO>>> AddBudget(AddBudgetDTO budgelist)
        {
            var budgetItem = _mapper.Map<Budget>(budgelist);
            _budgetdatacontext.Budgets.Add(budgetItem);
            await _budgetdatacontext.SaveChangesAsync();
            var budgetItems = await _budgetdatacontext.Budgets.ToListAsync();
            var addedBudgetDTOs = _mapper.Map<List<GetBudgetDTO>>(budgetItems);

            return Ok(addedBudgetDTOs);
        }

        [HttpGet("{projectId}/budget-report")]
        public async Task<ActionResult<List<GetBudgetDTO>>> GetBudgetReport(int projectId)
        {
            var budgetreport = _budgetdatacontext.Budgets.Where(report => report.ProjectId == projectId).ToList();

            return Ok(budgetreport);
        }


    }
}
