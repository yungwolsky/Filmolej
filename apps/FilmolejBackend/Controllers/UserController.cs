using FilmolejBackend.Services.Interfaces;
using FilmolejBackend.Requests;
using FilmolejBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FilmolejBackend.Controllers
{
    [Route("api/users")]
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
    }
}
