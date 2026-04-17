using FilmolejBackend.Services.Interfaces;
using FilmolejBackend.Requests;
using FilmolejBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FilmolejBackend.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegistrationRequest request)
        {
            var result = await _userService.AddUserAsync(
                request.Username,
                request.Email,
                request.Password
            );

            if (!result)
            {
                return StatusCode(500, new { message = "User registration failed" });
            }

            return Ok(new { message = "User successfully registered" });
        }
    }
}
