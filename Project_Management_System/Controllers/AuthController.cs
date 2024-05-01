using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using Project_Management_System.DTOs;
using Project_Management_System.Models;
using Project_Management_System.Data;
using System.Security.Cryptography;

namespace Project_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly DataContext _dataContext;

        public AuthController(IConfiguration configuration, DataContext dataContext)
        {
            _configuration = configuration;
            _dataContext = dataContext;
        }

        [HttpPost("login")]
        public ActionResult<string> Login(UserLoginDto request)
        {
            // Assuming you retrieve user data from the database
            User user = _dataContext.Users.FirstOrDefault(u => u.UserName == request.UserName);

            // Check if the user exists
            if (user == null)
            {
                return BadRequest(new { message = "Enter valid User name." });
            }

            // Check if password matches
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return BadRequest(new { message = "Incorrect password" });
            }
            //string userCategoryType = user.UserCategory?.UserCategoryType;

            /* string userCategoryType = _dataContext.UserCategory
                                         .Where(uc => uc.UserCategoryId == user.UserCategoryId)
                                         .Select(uc => uc.UserCategoryType)
                                         .FirstOrDefault();*/
            // Generate JWT token
            string token = GenerateAccessJwtToken(user);

            // Return token to the client
            return Ok(token);
        }

        private string GenerateAccessJwtToken(User user)
        {

            List<Claim> claims = new List<Claim>
            {
                new Claim("User ID",user.UserId.ToString()),
                new Claim("UserName", user.UserName),
                new Claim("UserCategoryId", user.UserCategoryId.ToString()),

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddHours(24),
                    signingCredentials: creds
                 );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

    }
}
