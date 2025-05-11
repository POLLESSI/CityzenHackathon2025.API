using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CitizenHackathon2025.BLL.Interfaces;
using CitizenHackathon2025.DAL.Entities;
using CitizenHackathon2025.Shared.DTOs;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CityzenHackathon2025.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (user == null || string.IsNullOrEmpty(user.Email) || user.PasswordHash == null)
                return BadRequest("Email and password are required.");

            var result = await _userService.RegisterUserAsync(user.Email, user.PasswordHash, user.Role);

            if (!result)
                return Conflict("Email already exists or registration failed.");

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            if (string.IsNullOrWhiteSpace(loginDto.Email) || string.IsNullOrWhiteSpace(loginDto.Password))
                return BadRequest("Email and password are required.");

            bool result = await _userService.LoginAsync(loginDto);

            if (!result)
                return Unauthorized("Invalid credentials.");

            return Ok("Connection successful.");
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetAllActiveUsers()
        {
            var users = await _userService.GetAllActiveUsersAsync();
            return Ok(users);
        }

        [HttpGet("getbyemail/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null)
                return NotFound("Utilisateur non trouvé.");

            return Ok(user);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPatch("role/{id}")]
        public IActionResult SetUserRole(int id, [FromQuery] string role)
        {
            _userService.SetRole(id, role);
            return Ok("User role updated.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeactivateUser(int id)
        {
            await _userService.DeactivateUserAsync(id);
            return Ok("User deactivated.");
        }
    }
}
