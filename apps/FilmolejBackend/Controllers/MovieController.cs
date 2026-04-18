using FilmolejBackend.Requests;
using FilmolejBackend.Services;
using FilmolejBackend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FilmolejBackend.Controllers
{
    [Route("api/movie")]
    [ApiController]
    public class MovieController(IMovieService movieService) : ControllerBase
    {
        private readonly IMovieService _movieService = movieService;

        [Authorize]
        [HttpPost("upload")]
        public async Task<IActionResult> UploadMovie([FromForm] MovieUploadRequest request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(userIdClaim == null)
            {
                return Unauthorized();
            }

            int userId = int.Parse(userIdClaim);
            var result = await _movieService.UploadFileAsync(userId, request.Title, request.File);

            if(!result)
            {
                return Problem("Movie upload failed");
            }

            return Ok(new { message = "Movie uploaded" });
        }
    }
}
