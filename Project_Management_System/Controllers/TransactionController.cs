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
                    var newIncome = await addincome(invoice.Value, projectId);
                    var transac = new Transaction //Add values to new transaction object
                    {
                        Value = invoice.Value,
                        Description = invoice.Description,
                        Type = invoice.Type,
                        Income = newIncome,
                        Expence = 0,
                        Date = invoice.Date,
                        ProjectId = projectId
                    };
                    _transacdatacontext.Transactions.Add(transac);
                    await _transacdatacontext.SaveChangesAsync();
                    return Ok(transac);
                }
                else if (transactype == "Expence")
                {

                    var newExpence = await addexpence(invoice.Value, projectId);
                    var transac = new Transaction
                    {
                        Value = invoice.Value,
                        Description = invoice.Description,
                        Type = invoice.Type,
                        Expence = newExpence,
                        Income = 0,
                        Date = invoice.Date,
                        ProjectId = projectId
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

        [HttpGet("Projects/{projectId}")]
        public async Task<ActionResult<List<GetTransacDto>>> Gettransac(int projectId)
        {
            try
            {
                var ProjectId = await _transacdatacontext.Projects.FindAsync(projectId);
                if (ProjectId == null)
                {
                    throw new Exception("Project not exist");
                }
                var transacdetails = await _transacdatacontext.Transactions.Where(t => t.ProjectId == projectId).ToListAsync();
                var transacdto = _mapper.Map<List<GetTransacDto>>(transacdetails);
                return Ok(transacdto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete("Transaction/{transacId}")]
        public async Task<ActionResult<List<Transaction>>> Deletetransac(int transacId)
        {
            var transactodelete = await _transacdatacontext.Transactions.FindAsync(transacId);
            if (transactodelete == null)
            {
                return NotFound();
            }
            _transacdatacontext.Transactions.Remove(transactodelete);
            await _transacdatacontext.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("Transaction/{transacId}")]
        public async Task<ActionResult<List<AddTransacDto>>> Updatetransac(double value,string type,string discription, int transacId,DateTime? date)
        {
            try
            {
                var exsistingtarnsac = await _transacdatacontext.Transactions.FindAsync(transacId);
                if (exsistingtarnsac == null)
                {
                    return BadRequest();
                }
                if (!string.IsNullOrEmpty(discription))
                {
                    exsistingtarnsac.Description = discription;
                }
                if (date.HasValue)
                {
                    exsistingtarnsac.Date = date.Value;
                }
                if (value !=0)
                {
                    var currentValue = exsistingtarnsac.Value;
                    var difference = value - currentValue;
                    if (exsistingtarnsac.Type == type)
                    {
                        if (type == "Income")
                        {
                        await UpdateIncome(difference, transacId);

                        }else if (type == "Expence")
                        {
                           await UpdateExpence(difference, transacId);
                        }
                        await _transacdatacontext.SaveChangesAsync();
                    }else
                    {
                        await UpdateIncomeORExpence(value,type, transacId);
                    }
                    exsistingtarnsac.Value = value;
                }else
                {
                    if (!string.IsNullOrEmpty(type))
                    {

                        if (exsistingtarnsac.Type != type)
                        {
                            var currentvalue = exsistingtarnsac.Value;
                            await UpdateIncomeORExpence(currentvalue,type, transacId);
                            exsistingtarnsac.Type = type;
                        }
                    }
                    else
                    {
                        exsistingtarnsac.Type = exsistingtarnsac.Type;
                    }
                }
                _transacdatacontext.Entry(exsistingtarnsac).State = EntityState.Modified;
                await _transacdatacontext.SaveChangesAsync();
                return Ok(exsistingtarnsac);
            }


            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        private async Task<double> addincome(double value, int id)
        {
            var currentIncome = await _transacdatacontext.Transactions    //Calculate current total income
           .Where(i => i.ProjectId == id && i.Type == "Income")
    .       SumAsync(i => i.Value);
            var newIncome = currentIncome + value;
            return newIncome;
        }
        private async Task<double> addexpence(double value, int id)
        {
            var currentExpence = await _transacdatacontext.Transactions    //Calculate current total income
           .Where(i => i.ProjectId == id && i.Type == "Expence")
           .SumAsync(i => i.Value);
            var newExpence = currentExpence + value;
            return newExpence;
        }

        private async Task<ActionResult<Transaction>> UpdateIncome(double value, int id)
        {
            var transaction = await _transacdatacontext.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            transaction.Income += value;
            await _transacdatacontext.SaveChangesAsync();
            return Ok(transaction);
        }

        private async Task<ActionResult<Transaction>> UpdateExpence(double value, int id)
        {
            var transaction = await _transacdatacontext.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            transaction.Expence += value;
            await _transacdatacontext.SaveChangesAsync();
            return Ok(transaction);
        }

        private async Task<ActionResult<Transaction>> UpdateIncomeORExpence(double updateval,string type, int id)
        {
            var transaction = await _transacdatacontext.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            
            if (type == "Income")
            {
                transaction.Expence -= transaction.Value;
                await UpdateIncome(updateval, id);
                
            }else if (type == "Expence")
            {
               transaction.Income -= transaction.Value;
               await UpdateExpence(updateval, id);
            }
            await _transacdatacontext.SaveChangesAsync();
            return Ok(transaction);
        }

    }
}










/*var excistingtransac = await _transacdatacontext.Transactions.FirstOrDefaultAsync(t => t.TransacId == transacId);
if (excistingtransac == null)
{
    return BadRequest();
}
var currentIncome = await _transacdatacontext.Transactions
.Where(i => i.TransacId == transacId && i.Type == "Income")
.SumAsync(i => i.Value);
var currentExpence = await _transacdatacontext.Transactions
.Where(i => i.TransacId == transacId && i.Type == "Expence")
.SumAsync(i => i.Value);


// excistingtransac.Description = updatetransac.Description != null && updatetransac.Type == null && updatetransac.Value == 0 ? updatetransac.Description : excistingtransac.Description;

if (updatetransac.Description == null && updatetransac.Value != 0 && updatetransac.Type != null) //value and type both are changed ans dis not changed
{

    if (updatetransac.Type == "Income")
    {
        excistingtransac.Expence = currentExpence - excistingtransac.Value;
        excistingtransac.Income = currentIncome + updatetransac.Value + 3000;
    }
    else if (updatetransac.Type == "Expence")
    {
        excistingtransac.Income = currentIncome - excistingtransac.Value;
        excistingtransac.Expence = currentExpence + updatetransac.Value + 3000;
    }
    excistingtransac.Type = updatetransac.Type;
    excistingtransac.Value = updatetransac.Value;
    _transacdatacontext.Transactions.Update(excistingtransac);
    await _transacdatacontext.SaveChangesAsync();


}
return Ok("Updated");
 else if (updatetransac.Description != null && updatetransac.Value != 0 && updatetransac.Type != null) //value and type and dsiscripton are changed
  {
      excistingtransac.Description = updatetransac.Description;
      excistingtransac.Value = updatetransac.Value;
      excistingtransac.Type = updatetransac.Type;


      if (updatetransac.Type == "Income")
      {

          excistingtransac.Income = currentIncome + excistingtransac.Value;
      }
      else if (updatetransac.Type == "Expence")
      {

          excistingtransac.Expence = currentExpence + excistingtransac.Value;
      }
      return Ok(excistingtransac);
  }
else  if (updatetransac.Description == null && updatetransac.Value != 0 && updatetransac.Type == null)  //only value changed not type and discription not chaged
  {
      excistingtransac.Description = excistingtransac.Description;
      excistingtransac.Type = excistingtransac.Type;
      var updatedval = updatetransac.Value - excistingtransac.Value; //updated value calculate
      excistingtransac.Value = updatetransac.Value;
      if (excistingtransac.Type == "Income")
      {

          excistingtransac.Income = currentIncome + updatedval;
      }
      else if (excistingtransac.Type == "Expence")
      {

          excistingtransac.Expence = currentExpence + updatedval;
      }

  }
  else if (updatetransac.Description == null && updatetransac.Value == 0 && updatetransac.Type != null && excistingtransac.Type != updatetransac.Type) //value and dis not changed and only type change
  {
      excistingtransac.Description = excistingtransac.Description;
      excistingtransac.Value = excistingtransac.Value;
      if (excistingtransac.Type == "Income" && updatetransac.Type == "Expence")
      {
          excistingtransac.Income = currentIncome - excistingtransac.Value;
          excistingtransac.Expence = currentExpence + excistingtransac.Value;
      }
      else if (excistingtransac.Type == "Expence" && updatetransac.Type == "Income")
      {
          excistingtransac.Expence = currentExpence - excistingtransac.Value;
          excistingtransac.Income = currentIncome + excistingtransac.Value;
      }
      return Ok(excistingtransac);
  }
  else if (updatetransac.Description != null && updatetransac.Value == 0 && updatetransac.Type != null && excistingtransac.Type != updatetransac.Type) //value not changed and only and discription type change
  {
      excistingtransac.Description = updatetransac.Description;
      excistingtransac.Value = excistingtransac.Value;
      if (excistingtransac.Type == "Income" && updatetransac.Type == "Expence")
      {
          excistingtransac.Income = currentIncome - excistingtransac.Value;
          excistingtransac.Expence = currentExpence + excistingtransac.Value;
      }
      else if (excistingtransac.Type == "Expence" && updatetransac.Type == "Income")
      {
          excistingtransac.Expence = currentExpence - excistingtransac.Value;
          excistingtransac.Income = currentIncome + excistingtransac.Value;
      }
      return Ok(excistingtransac);
  }
  else  //any not selected
  {
      excistingtransac.Value = excistingtransac.Value;
      excistingtransac.Type = excistingtransac.Type;
      excistingtransac.Description = excistingtransac.Description;
      return Ok(excistingtransac);
  }

   _transacdatacontext.Transactions.Update(excistingtransac);
   await _transacdatacontext.SaveChangesAsync();
   return Ok(excistingtransac);*/