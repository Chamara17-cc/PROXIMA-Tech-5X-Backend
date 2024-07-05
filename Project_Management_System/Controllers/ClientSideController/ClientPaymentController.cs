using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Management_System.Data;
using Project_Management_System.Models;

namespace Project_Management_System.Controllers.ClientSideController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientPaymentController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public ClientPaymentController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("GetClientsPayments")]
        public async Task<IActionResult> GetClientPayment(int userId)
        {
            var projects = await _dataContext.Projects.Where(e => e.ClientId == userId).Select(e => e.ProjectId).ToListAsync();
            Console.WriteLine(projects);
            var payments = new List<ClientPayment>();
            foreach (var project in projects)
            {
                var payment = await _dataContext.ClientPayments.Where(e => e.ProjectId == project && (e.Mode == "accepted" || e.Mode == "accept")).ToListAsync();
                payments.AddRange(payment);
            }

            var paymentdtos=new List<ClientPayementDto>();

            foreach(var x in payments)
            {
                var z = await _dataContext.Projects.FirstOrDefaultAsync(e => e.ProjectId == x.ProjectId);

                var paymentdto = new ClientPayementDto
                {
                    ProjectId = x.ProjectId,
                    PaymentId = x.PaymentId,
                    Date = x.PaymentDate,
                    Mode = x.Mode,
                    PaymentAmount = x.Payment,
                    ProjectName = z.ProjectName,
                    status = x.Status
                };
                paymentdtos.Add(paymentdto);
            }

            return Ok(paymentdtos);
        }

    }
}
