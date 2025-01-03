using Microsoft.AspNetCore.Mvc;
using truedrive_backend.Data;
using truedrive_backend.Models;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace truedrive_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly TrueDriveContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(TrueDriveContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            var user = new User
            {
                FullName = registerModel.FullName,
                Email = registerModel.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerModel.Password),
                Address = registerModel.Address,
                PhoneNumber = registerModel.PhoneNumber,
                Role = "Customer" // Default role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Generate JWT token
            var token = GenerateJwtToken(user);

            return Ok(new
            {
                message = "User registered successfully",
                token,
                user = new
                {
                    userId = user.UserId,
                    fullName = user.FullName,
                    email = user.Email,
                    address = user.Address,
                    phone = user.PhoneNumber,
                    role = user.Role,
                }
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == login.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }

            // Generate JWT token
            var token = GenerateJwtToken(user);
            return Ok(new
            {
                message = "Login successful",
                token,
                user = new
                {
                    userId = user.UserId,
                    fullName = user.FullName,
                    email = user.Email,
                    address = user.Address,
                    phone = user.PhoneNumber,
                    role = user.Role,
                }
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromHeader(Name = "Authorization")] string authorization)
        {
            if (string.IsNullOrEmpty(authorization) || !authorization.StartsWith("Bearer "))
            {
                return BadRequest(new { message = "Invalid token" });
            }

            var token = authorization.Substring("Bearer ".Length).Trim();

            var tokenEntry = _context.Tokens.SingleOrDefault(t => t.JwtToken == token);
            if (tokenEntry == null)
            {
                return NotFound(new { message = "Token not found" });
            }

            tokenEntry.IsValid = false;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Logout successful" });
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            // Save the token to the database
            var tokenEntry = new Token
            {
                JwtToken = jwtToken,
                IsValid = true,
                CreatedAt = DateTime.Now,
                UserId = user.UserId
            };
            _context.Tokens.Add(tokenEntry);
            _context.SaveChanges();

            return jwtToken;
        }
    }

    public class RegisterModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

}