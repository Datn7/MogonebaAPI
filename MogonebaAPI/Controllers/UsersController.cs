using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MogonebaAPI.Models;
using MogonebaAPI.Data;

namespace MogonebaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher = new();

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/users/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegistrationDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Username) ||
                string.IsNullOrWhiteSpace(dto.Email) ||
                string.IsNullOrWhiteSpace(dto.Password))
            {
                return BadRequest("All fields are required.");
            }

            var emailExists = await _context.Users.AnyAsync(u => u.Email == dto.Email);
            if (emailExists)
            {
                return BadRequest("Email is already registered.");
            }

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "User registered successfully.",
                user = new { user.Id, user.Username, user.Email }
            });
        }

        // POST: api/users/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                return Unauthorized("Invalid email or password.");
            }

            return Ok(new
            {
                message = "Login successful.",
                user = new { user.Id, user.Username, user.Email }
            });
        }
    }
}
