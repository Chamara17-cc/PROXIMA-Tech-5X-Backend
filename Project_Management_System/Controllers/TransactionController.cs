using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Management_System.Data;
using Project_Management_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly DataContext _transacdatacontext;
        private readonly IMapper _mapper;

        public TransactionController(DataContext datacontext, IMapper mapper)
        {
            _transacdatacontext = datacontext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetProjectDto>>> GetProject()
        {
            var projectlist = await _transacdatacontext.Projects.ToListAsync();
            return Ok(projectlist);
        }

        [HttpPost("Project/{projectId}")]
        public async Task<ActionResult<List<Transaction>>> AddTransactions(AddTransacDto invoice, int projectId)
        {
            try
            {
                var i_project = await _transacdatacontext.Projects.FindAsync(projectId);
                if (i_project == null)
                {
                    return NotFound("Project not found");
                }

                var transactype = invoice.Type;
                if (transactype == "Income")
                {
                    var currentIncome = await _transacdatacontext.Transactions    //Calculate current total income
                        .Where(i => i.ProjectId == projectId && i.Type == "Income")
                        .SumAsync(i => i.Value);

                    var newIncome = currentIncome + invoice.Value;
                    var transac = new Transaction //Add values to new transaction object
                    {
                        Value = invoice.Value,
                        Description = invoice.Description,
                        Type = invoice.Type,
                        Income = newIncome,
                        Expence = 0,
                        ProjectId = projectId
                    };
                    _transacdatacontext.Transactions.Add(transac);
                    await _transacdatacontext.SaveChangesAsync();
                    return Ok(transac);
                }
                else if (transactype == "Expence")
                {
                    var currentExpence = await _transacdatacontext.Transactions
                        .Where(i => i.ProjectId == projectId && i.Type == "Expence")  //Calculate current total expence
                        .SumAsync(i => i.Value);

                    var newExpence = currentExpence + invoice.Value;
                    var transac = new Transaction
                    {
                        Value = invoice.Value,
                        Description = invoice.Description,
                        Type = invoice.Type,
                        Expence = newExpence,
                        Income = 0,
                        ProjectId = projectId // Assuming ProjectId needs to be set
                    };
                    _transacdatacontext.Transactions.Add(transac);
                    await _transacdatacontext.SaveChangesAsync();
                    return Ok(transac);
                }
                else
                {
                    return BadRequest("Invalid transaction type");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
