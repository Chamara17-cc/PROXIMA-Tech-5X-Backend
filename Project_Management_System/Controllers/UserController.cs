using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MimeKit;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X9;
using Project_Management_System.Data;
using Project_Management_System.DTOs;
using Project_Management_System.Models;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Project_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnvironment;

        public UserController(DataContext dataContext, IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            this._hostEnvironment = hostEnvironment;
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> RegisterUser([FromForm] UserRegisterDto request)
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

            string imageName = null;
            if (request.ImageFile != null)
            {
                imageName = await SaveImage(request.ImageFile);
            }

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
                ContactNumber = request.ContactNumber,
                Email = request.Email,
                JobRoleId = JobRoleId,
                UserCategoryId = UserCategoryId,
                ProfileImageName = imageName

            };

            _dataContext.Users.Add(newUser);
            await _dataContext.SaveChangesAsync();

            // Get the newly created user's UserId
            int newUserId = newUser.UserId;

            // Add the UserId to the relevant table based on UserCategoryType
            switch (request.UserCategoryType)
            {
                case "Admin":
                    var newAdmin = new Admin
                    {
                        AdminId = newUserId,

                    };
                    _dataContext.Admins.Add(newAdmin);
                    break;

                case "Manager":
                    var newProjectManager = new ProjectManager
                    {
                        ProjectManagerId = newUserId,

                    };
                    _dataContext.ProjectManagers.Add(newProjectManager);
                    break;

                case "Developer":
                    var newDeveloper = new Developer
                    {
                        DeveloperId = newUserId,
                        FinanceReceiptId = 1,
                        TotalDeveloperWorkingHours = 0,

                    };
                    _dataContext.Developers.Add(newDeveloper);
                    break;

            }

            await _dataContext.SaveChangesAsync();

            return randomPassword;
        }

        [HttpGet("{id:int}")]
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
                ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, user.ProfileImageName),
                ProfileImageName = user.ProfileImageName,
                IsActive = user.IsActive,
                ContactNumber = user.ContactNumber,
                Email = user.Email,
                UserCategoryType = user.UserCategory?.UserCategoryType,
                JobRoleType = user.JobRole?.JobRoleType
            };

            return Ok(viewUserDto);
        }

        [HttpGet("list")]
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

        [HttpPost("deactivate-user")]
        public async Task<IActionResult> DeactivateUser([FromBody] DeactivateUserDto request)
        {
            // Fetch the user from the database using the username
            var user = _dataContext.Users.FirstOrDefault(u => u.UserId == request.UserId);

            // Check if the user exists
            if (user == null)
            {
                return BadRequest(new { message = "User not found." });
            }

            if (user.IsActive == false)
            {
                return BadRequest(new { message = "User already deactivated from the system." });
            }

            user.IsActive = false;

            // Save the changes to the database
            _dataContext.Users.Update(user);
            await _dataContext.SaveChangesAsync();

            return Ok(new { message = "User successfully deactivate.!" });
        }

        [HttpPost("reactivate-user")]
        public async Task<IActionResult> ReactivateUser([FromBody] ReactivateUserDto request)
        {
            // Fetch the user from the database using the username
            var user = _dataContext.Users.FirstOrDefault(u => u.UserId == request.UserId);

            // Check if the user exists
            if (user == null)
            {
                return BadRequest(new { message = "User not found." });
            }

            if (user.IsActive == true)
            {
                return BadRequest(new { message = "User already reactivated from the system." });
            }

            user.IsActive = true;

            // Save the changes to the database
            _dataContext.Users.Update(user);
            await _dataContext.SaveChangesAsync();

            return Ok(new { message = "User successfully reactivate.!" });
        }

        [HttpPut("update/{userId}")]
        public async Task<ActionResult> UpdateUserProfile(int userId, [FromForm] UserUpdateDto request)
        {
            var user = await _dataContext.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            // Check if the email is already taken by another user
            var existingUser = _dataContext.Users.FirstOrDefault(u => u.UserId != userId);


            user.Email = request.Email;
            user.ContactNumber = request.ContactNumber;
            user.Address = request.Address;

            if (request.ImageFile != null)
            {
                // Save new profile image
                var imageName = await SaveImage(request.ImageFile);
                user.ProfileImageName = imageName;
            }

            _dataContext.Users.Update(user);
            await _dataContext.SaveChangesAsync();

            return Ok(new { message = "Profile updated successfully" });
        }

        [HttpGet("search")]
        public ActionResult<List<ViewUserListDto>> SearchUsers([FromQuery] string term)
        {
            // If the term is empty, return a bad request
            if (string.IsNullOrWhiteSpace(term))
            {
                return BadRequest(new { message = "Search term cannot be empty" });
            }

            // Convert term to lowercase for case-insensitive comparison
            term = term.ToLower();

            var users = _dataContext.Users
                .Where(u => u.UserId.ToString().ToLower().Contains(term) ||
                            u.UserName.ToLower().Contains(term) ||
                            u.UserCategory.UserCategoryType.ToLower().Contains(term))
                .Select(u => new ViewUserListDto
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    Email = u.Email,
                    UserCategoryType = u.UserCategory.UserCategoryType
                })
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

        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }





    }
}



