using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using eComWebApp.Data;
using eComWebApp.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace eComWebApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<UsersController> _logger;
        private readonly IEmailService _emailService;

        public UsersController(ApplicationDbContext context, UserManager<User> userManager, ILogger<UsersController> logger, IEmailService emailService)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserGetDto>>> GetAll()
        {
            var users = await _context.Users
                .Select(x => new UserGetDto
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    DateOfBirth = x.DateOfBirth
                })
                .ToListAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserGetDto>> GetById(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var userDto = new UserGetDto
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth
            };

            return Ok(userDto);
        }

        [HttpPost]
        public async Task<ActionResult<UserGetDto>> Create([FromBody] UserCreateDto newUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newUser = new User
            {
                UserName = newUserDto.UserName,
                FirstName = newUserDto.FirstName,
                LastName = newUserDto.LastName,
                Email = newUserDto.Email,
                DateOfBirth = newUserDto.DateOfBirth
            };

            var result = await _userManager.CreateAsync(newUser, newUserDto.Password);

            if (result.Succeeded)
            {
                var userDto = new UserGetDto
                {
                    Id = newUser.Id,
                    UserName = newUser.UserName,
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    Email = newUser.Email,
                    DateOfBirth = newUser.DateOfBirth
                };

                return CreatedAtAction(nameof(GetById), new { id = newUser.Id }, userDto);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserGetDto>> Edit(int id, [FromBody] UserCreateDto updatedUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await _context.Users.FindAsync(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.UserName = updatedUserDto.UserName;
            existingUser.FirstName = updatedUserDto.FirstName;
            existingUser.LastName = updatedUserDto.LastName;
            existingUser.Email = updatedUserDto.Email;
            existingUser.DateOfBirth = updatedUserDto.DateOfBirth;

            var result = await _userManager.UpdateAsync(existingUser);

            if (result.Succeeded)
            {
                var userDto = new UserGetDto
                {
                    Id = existingUser.Id,
                    UserName = existingUser.UserName,
                    FirstName = existingUser.FirstName,
                    LastName = existingUser.LastName,
                    Email = existingUser.Email,
                    DateOfBirth = existingUser.DateOfBirth
                };

                return Ok(userDto);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);

            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return Unauthorized("Invalid username or password.");
            }

            return Ok(new { message = "Login successful" });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto request)
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                _logger.LogError("Email is null or empty in forgot password request.");
                return BadRequest("Email is required.");
            }

            _logger.LogInformation($"Received forgot password request for email: {request.Email}");

            var user = await _userManager.Users
                                             .SingleOrDefaultAsync(u => u.Email.ToLower() == request.Email.ToLower());

            if (user == null)
            {
                _logger.LogError($"No user found with email {request.Email}");
                return BadRequest("User not found");
            }

            _logger.LogInformation($"User details: Id={user.Id}, Email={user.Email}, UserName={user.UserName}");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            if (token == null)
            {
                _logger.LogError("Failed to generate password reset token.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to generate password reset token.");
            }

            _logger.LogInformation($"Generated token: {token}");

            var resetLink = Url.Action("ResetPassword", "Users", new { token, email = user.Email }, Request.Scheme);

            await _emailService.SendEmailAsync(user.Email, "Password Reset", $"<a href='{resetLink}'>Reset Password</a>");

            return Ok("Password reset link has been sent to your email.");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Token) || string.IsNullOrEmpty(request.NewPassword))
            {
                return BadRequest("Email, token, and new password are required.");
            }

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("Password has been reset.");
        }
    }
}
