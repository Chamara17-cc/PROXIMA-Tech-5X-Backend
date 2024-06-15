using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Org.BouncyCastle.Asn1.Pkcs;
using Project_Management_System.Configuration;
using Project_Management_System.Data;
using Project_Management_System.DTOs;
using Project_Management_System.Models;
using System.Security.Cryptography;
using System.Text;

namespace Project_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        //private readonly MailSettings _mailSettings;
        public UserController(IConfiguration configuration, DataContext dataContext, IMapper mapper)
        {
            _configuration = configuration;
            _dataContext = dataContext;
            _mapper = mapper;
            // _mailSettings = mailSettings;
        }

        
        [HttpPost("register"), Authorize(Roles = "1")]
        public async Task<ActionResult<string>> RegisterUser(UserRegisterDto request)
        {
            var randomPassword = CreateRandomPassword(10);
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(randomPassword);

            var existingUser = _dataContext.Users.FirstOrDefault(u => u.UserName == request.UserName);
            if (existingUser != null)
            {
                return BadRequest(new { message = "Username already exists" });
            }

            var userCategory = _dataContext.UsersCategories.FirstOrDefault(uc => uc.UserCategoryType == request.UserCategoryType);
            if (userCategory == null)
            {
                return BadRequest(new { message = "Invalid UserCategoryType" });
            }

            int UserCategoryId = userCategory.UserCategoryId;

            var jobRole = _dataContext.JobRoles.FirstOrDefault(jr => jr.JobRoleType == request.JobRoleType);

            if (jobRole == null)
            {
                return BadRequest(new { message = "Invalid jobRoleType" });
            }

            int JobRoleId = jobRole.JobRoleId;

            string refreshToken = GenerateRefreshToken();

            // Assuming you have a User model and a database context
            User newUser = new User
            {
                UserName = request.UserName,
                PasswordHash = passwordHash,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Address = request.Address,
                Gender = request.Gender,
                NIC = request.NIC,
                DOB = request.DOB,
                // ProfilePictureLink = request.ProfilePictureLink,
                ContactNumber = request.ContactNumber,
                Email = request.Email,
                JobRoleId = JobRoleId,
                UserCategoryId = UserCategoryId,
                RefreshToken = refreshToken,
                RefreshTokenExpiryTime = DateTime.Now.AddDays(7)
        };

            // Save the new user to the database
            _dataContext.Users.Add(newUser);
            _dataContext.SaveChanges();

           // return (randomPassword);
            // await SendPasswordEmail(request.Email,request.UserName, randomPassword);

           return Ok(new { message = "User registered successfully. Email sent with password." });

        }

        [HttpGet("{id:int}"), Authorize(Roles = "1,2")]
        public async Task<ActionResult<ViewUserDetailDto>> GetById(int id)
        {
            var user = await _dataContext.Users
                .Include(u => u.UserCategory)
                .Include(u => u.JobRole)
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            // Map the user data along with UserCategoryType and JobRoleType to ViewUserDetailDto
            var viewUserDto = new ViewUserDetailDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                Gender = user.Gender,
                NIC = user.NIC,
                DOB = user.DOB,
                // ProfilePictureLink = user.ProfilePictureLink,
                IsActive = user.IsActive,
                ContactNumber = user.ContactNumber,
                Email = user.Email,
                UserCategoryType = user.UserCategory?.UserCategoryType,
                JobRoleType = user.JobRole?.JobRoleType
            };

            return Ok(viewUserDto);
        }

        [HttpGet("list"), Authorize(Roles = "1,2")]
        public ActionResult<IEnumerable<ViewUserListDto>> GetAll()
        {
            var users = _dataContext.Users
                .Include(u => u.UserCategory)
                .ToList();

            // Map each user to ViewUserListDto
            var viewUserListDtos = users.Select(user => new ViewUserListDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                UserCategoryType = user.UserCategory != null ? user.UserCategory.UserCategoryType : null
            }).ToList();

            return Ok(viewUserListDtos);
        }


        [HttpGet("search")]
        public ActionResult<List<User>> SearchUsers([FromQuery] string term)
        {
            var users = _dataContext.Users
                .Where(u => u.UserId.ToString().Contains(term) || u.UserName.Contains(term))
                .ToList();

            if (users == null || users.Count == 0)
            {
                return NotFound(new { message = "No users found" });
            }

            return Ok(users);
        }


        public static string CreateRandomPassword(int PasswordLength)
        {
            string _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
            Random randNum = new Random();
            char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;
            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        /* private async Task SendPasswordEmail(string userEmail, string userName, string password)
         {
             try
             {
                 using var client = new MailKit.Net.Smtp.SmtpClient();
                 await client.ConnectAsync(_mailSettings.Server, _mailSettings.Port, false);
                 await client.AuthenticateAsync(_mailSettings.UserName, _mailSettings.Password);

                 var message = new MimeMessage();
                 message.From.Add(new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail));
                 message.To.Add(new MailboxAddress(userEmail, userEmail)); // Use email address as both name and address
                 message.Subject = "Your Password";

                 // Include the user's name in the email body
                 var text = $"Dear {userName},\n\nYour password is: {password}";
                 message.Body = new TextPart("plain")
                 {
                     Text = text
                 };


                 await client.SendAsync(message);
                 await client.DisconnectAsync(true);

                 // Save the mail data to your database
                 var mailData = new MailData
                 {
                     EmailToId = userEmail,
                     EmailToName = userName,
                     EmailSubject = "Your Password",
                     EmailBody = text
                 };
                 _dataContext.MailData.Add(mailData);
                 await _dataContext.SaveChangesAsync();
             }
             catch (Exception ex)
             {
                 // Log or handle the exception as needed
                 Console.WriteLine($"Error sending email: {ex.Message}");
                 throw;
             }
         }*/


        /*  public async Task<IActionResult> RegisterUserAndSendEmail(UserRegisterDto userDto)
          {
              var result = await RegisterUser(userDto); // Register the user and get the ActionResult<string>
              if (result.Result is BadRequestObjectResult badRequest)
              {
                  // Handle bad request if needed
                  return BadRequest(badRequest.Value);
              }
              var randomPassword = result.Value; // Extract the string value from ActionResult<string>
              await SendPasswordEmail(userDto.Email, userDto.UserName, randomPassword); // Send email to the user with the password
              return Ok("User registered successfully, and email sent with password."); // Return OK status
          }
  */


    }
}



