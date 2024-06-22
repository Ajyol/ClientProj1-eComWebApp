using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eComWebApp.Server.Models;
using eComWebApp.Data;

namespace eComWebApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public UsersController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
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
                return NotFound(); // Return 404 Not Found if the user is not found
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
            var newUser = new User
            {
                UserName = newUserDto.UserName,
                FirstName = newUserDto.FirstName,
                LastName = newUserDto.LastName,
                Email = newUserDto.Email,
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
        public async Task<ActionResult<UserGetDto>> Edit(int id, [FromBody] UserCreateDto updatedUser)
        {
            var existingUser = await _context.Users.FindAsync(id);

            if (existingUser == null)
            {
                return NotFound(); // Return 404 Not Found if the user is not found
            }

            existingUser.UserName = updatedUser.UserName;
            existingUser.FirstName = updatedUser.FirstName;
            existingUser.LastName = updatedUser.LastName;
            existingUser.Email = updatedUser.Email;

            await _context.SaveChangesAsync();

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

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound(); // Return 404 Not Found if the user is not found
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent(); // Return 204 No Content on successful deletion
        }
    }
}
