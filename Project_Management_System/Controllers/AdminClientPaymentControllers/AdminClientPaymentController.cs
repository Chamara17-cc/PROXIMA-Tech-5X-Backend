using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Management_System.Controllers.ClientSideControllers;
using Project_Management_System.Data;
using Project_Management_System.Migrations;

namespace Project_Management_System.Controllers.AdminClientPaymentControllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class AdminClientPaymentController : ControllerBase
    {

        private readonly DataContext _dataContext;
        public AdminClientPaymentController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet("GetClientPayments")]
        public async Task<IActionResult> GetClientPayments()
        {
            var payments = await _dataContext.ClientPayments.ToListAsync();
            var paymentlist = new List<ClientPayementDto>();
            foreach (var payment in payments)
            { 
                var project = await _dataContext.Projects.FirstOrDefaultAsync(e => e.ProjectId == payment.ProjectId);
                var paymentdto = new ClientPayementDto
                {
                    PaymentId=payment.PaymentId,
                    ProjectId = project.ProjectId,
                    ProjectName = project.ProjectName,
                    PaymentAmount = payment.Payment,
                    Date = payment.PaymentDate,
                    Mode = payment.Mode,
                    status = payment.Status,
                };
                paymentlist.Add(paymentdto);
            }
            return Ok(paymentlist);
        }
        [HttpGet("GetClientProjects")]
        public async Task<IActionResult> GetProject()
        {
            var projects = await _dataContext.Projects.ToListAsync();
            return Ok(projects);

        }

        [HttpGet("PaymentAccept")]
        public async Task<IActionResult> PaymentAccept(int id)
        {
            var payement = await _dataContext.ClientPayments.FirstOrDefaultAsync(e => e.PaymentId == id);
            if (payement == null)
            {
                return BadRequest("Payment not found");
            }
            payement.Mode = "accepted";
            await _dataContext.SaveChangesAsync();
            return Ok(true);
        }

        [HttpGet("PaymentReject")]
        public async Task<IActionResult> PaymentReject(int id)
        {
            var payement = await _dataContext.ClientPayments.FirstOrDefaultAsync(e => e.PaymentId == id);
            if (payement == null)
            {
                return BadRequest("Payment not found");
            }
            payement.Mode = "rejected";
            await _dataContext.SaveChangesAsync();
            return Ok(true);
        }
    }
}
