﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
       
        public UserController( DataContext dataContext, IMapper mapper)
        {         
            _dataContext = dataContext;
            _mapper = mapper;
        }


        [HttpPost("register")]
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
            };

            // Save the new user to the database
            _dataContext.Users.Add(newUser);
            _dataContext.SaveChanges();

            return (randomPassword);
            //return Ok(new { message = "User registered successfully", randomPassword }); 

        }

        [HttpPost("admin")]
        public async Task<ActionResult<Admin>> AddAdmin(AdminDTO request)
        {
            var newAdmin = new Admin
            {
                AdminId = request.AdminId
            };
            _dataContext.Add(newAdmin);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("projectManager")]
        public async Task<ActionResult<ProjectManager>> AddPManager(PmDTO request)
        {
            var newProjectM = new ProjectManager
            {
                ProjectManagerId = request.ProjectManagerId
            };

            _dataContext.Add(newProjectM);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("developer")]
        public async Task<ActionResult<Developer>> AddDeveloper(DevDTO request)
        {
            var newDev = new Developer
            {
                DeveloperId = request.DeveloperId,
                FinanceReceiptId = request.FinanceReceiptId,
                TotalDeveloperWorkingHours = 0
            };

            _dataContext.Add(newDev);
            await _dataContext.SaveChangesAsync();
            return Ok(new { message = "Developer added" });
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
                // ProfilePictureLink = user.ProfilePictureLink,
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

       
    }
}



