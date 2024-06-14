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

            // Generate JWT token
            string accessToken = GenerateAccessJwtToken(user);
            string refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            _dataContext.SaveChanges();

            var response = new AuthenticationResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            return Ok(response);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Retrieve the refresh token from the request
            var refreshToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            // Find the user associated with the refresh token
            var user = _dataContext.Users.FirstOrDefault(u => u.RefreshToken == refreshToken);

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid refresh token" });
            }

            // Clear the refresh token from the user
            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;
            _dataContext.SaveChanges();

            // Optionally, clear the refresh token from the client-side (e.g., browser)
            // This depends on how you store the refresh token on the client-side

            return Ok(new { message = "Logout successful" });
        }


        [HttpPost("refresh")]
        public ActionResult<AuthenticationResponseDto> Refresh(TokenRefreshRequestDto request)
        {
            var user = _dataContext.Users.FirstOrDefault(u => u.RefreshToken == request.RefreshToken);

            if (user == null || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return Unauthorized(new { message = "Invalid refresh token" });
            }

            string newAccessToken = GenerateAccessJwtToken(user);
            string newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            _dataContext.SaveChanges();

            var response = new AuthenticationResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken

            };

            return Ok(response);
        }

        private string GenerateAccessJwtToken(User user)
        {

            List<Claim> claims = new List<Claim>
            {
                new Claim("UserID", user.UserId.ToString()),
                new Claim("UserName", user.UserName),
                new Claim("UserCategory", user.UserCategoryId.ToString())

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
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
