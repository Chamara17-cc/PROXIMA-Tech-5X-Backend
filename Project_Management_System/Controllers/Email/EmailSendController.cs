using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Text;
using MimeKit;
using Project_Management_System.Data;
using MailKit.Net.Smtp;
using Project_Management_System.Models;

namespace Project_Management_System.Controllers.Email
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailSendController : ControllerBase
    {
        public readonly DataContext _context;

        public EmailSendController(DataContext _context)
        {
            this._context = _context;
        }

        [HttpPost]
        public IActionResult SendEmail(int adminId, int PmId, string projectName)
        {

            string adminName;
            User adName = _context.Users
                .Where(x => x.UserId == adminId)
                .FirstOrDefault();

            if (adName != null)
            {
                adminName = adName.Email;
            }
            else
            {
                adminName = null;
            }


            string receiver;
            User UserEmail = _context.Users
                .Where(x => x.UserId == PmId)
                .FirstOrDefault();

            if (UserEmail != null)
            {
                receiver = UserEmail.Email;
            }
            else
            {
                receiver = null; 
            }


            string pManager;
            User PmName = _context.Users
                .Where(x => x.UserId == adminId)
                .FirstOrDefault();

            if (PmName != null)
            {
                pManager = PmName.UserName;
            }
            else
            {
                pManager = null;
            }

            DateTime date = DateTime.Now;

             string body = "<h4>Dear " + pManager +",</h4><p>You have been assigned for a new project. Please check Proxima account for more info.</p><br><p>Project Name: " + projectName + "</p><p>Created Date: " + date + "</p><h4>Sincely,<br>" + adminName + "<br>Admin Division</h4>";

             var email = new MimeMessage();
             email.From.Add(MailboxAddress.Parse("surajmshan1234@gmail.com"));
             email.To.Add(MailboxAddress.Parse("surajmshan@gmail.com"));

             email.Subject = "New Project Assignment";
             email.Body = new TextPart(TextFormat.Html) { Text = body };

             using var smtp = new SmtpClient();
             smtp.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
             smtp.Authenticate("surajmshan1234@gmail.com", "zkvzwrfytxbjfdra");
             smtp.Send(email);
             smtp.Disconnect(true);

            return Ok();

        }

       /* [HttpPost("DeveloperAssign")]
        public IActionResult SendEmailtoDev(int adminId, int PmId, string projectName)
        {

            string adminName;
            User adName = _context.Users
                .Where(x => x.UserId == adminId)
                .FirstOrDefault();

            if (adName != null)
            {
                adminName = adName.Email;
            }
            else
            {
                adminName = null;
            }


            string receiver;
            User UserEmail = _context.Users
                .Where(x => x.UserId == PmId)
                .FirstOrDefault();

            if (UserEmail != null)
            {
                receiver = UserEmail.Email;
            }
            else
            {
                receiver = null;
            }


            string pManager;
            User PmName = _context.Users
                .Where(x => x.UserId == adminId)
                .FirstOrDefault();

            if (PmName != null)
            {
                pManager = PmName.UserName;
            }
            else
            {
                pManager = null;
            }

            DateTime date = DateTime.Now;

            string body = "<h4>Dear " + pManager + ",</h4><p>You have been assigned for a new project. Please check Proxima account for more info.</p><br><p>Project Name: " + projectName + "</p><p>Created Date: " + date + "</p><h4>Sincely,<br>" + adminName + "<br>Admin Division</h4>";

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("surajmshan1234@gmail.com"));
            email.To.Add(MailboxAddress.Parse("surajmshan@gmail.com"));

            email.Subject = "New Project Assignment";
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
            smtp.Authenticate("surajmshan1234@gmail.com", "zkvzwrfytxbjfdra");
            smtp.Send(email);
            smtp.Disconnect(true);

            return Ok();

        }*/

    }
}
