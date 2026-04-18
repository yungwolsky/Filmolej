using FilmolejBackend.Services.Interfaces;
using FilmolejBackend.Requests;
using FilmolejBackend.Models;
using Microsoft.AspNetCore.Http;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.Data;

namespace FilmolejBackend.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController(IUserService userService, ITokenService tokenService) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly ITokenService _tokenService = tokenService;

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegistrationRequest request)
        {
            var newUser = await _userService.AddUserAsync(
                request.Username,
                request.Email,
                request.Password
            );

            if (newUser == null)
            {
                return StatusCode(500, new { message = "User registration failed" });
            }

            var token = _tokenService.GenerateToken(newUser);

            return Ok(new { token, message = "User successfully registered" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginRequest request)
        {
            var user = await _userService.GetUserByEmailAsync(request.Email);

            if (user == null)
            {
                return Unauthorized(new { message = "Wrong e-mail or password!" });
            }

            if(!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return Unauthorized(new { message = "Wrong e-mail or password!" });
            }

            var token = _tokenService.GenerateToken(user);

            return Ok(new { token, message = "User logged in" });
        }
    }
}
