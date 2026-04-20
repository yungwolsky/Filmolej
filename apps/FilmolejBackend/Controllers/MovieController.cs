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
    public class MovieController(IMovieService movieService,
        ILogger<MovieController> logger,
        IConfiguration config) : ControllerBase
    {
        private readonly IMovieService _movieService = movieService;
        private readonly ILogger<MovieController> _logger = logger;
        private readonly string _basePath = config["Storage:BasePath"]!;

        [Authorize]
        [HttpPost("upload-chunk")]
        [RequestSizeLimit(50 * 1024 * 1024)]
        public async Task<IActionResult> UploadChunk(
            [FromForm] IFormFile file,
            [FromForm] string uploadId,
            [FromForm] int chunkIndex,
            [FromForm] int totalChunks,
            [FromForm] string fileName)
        {
            var tempDir = Path.Combine(_basePath, "chunks", uploadId);
            Directory.CreateDirectory(tempDir);

            var chunkPath = Path.Combine(tempDir, $"{chunkIndex}.part");

            await using var stream = new FileStream(chunkPath, FileMode.Create);
            await file.CopyToAsync(stream);

            return Ok();
        }

        [Authorize]
        [HttpPost("complete-upload")]
        public async Task<IActionResult> CompleteUpload([FromBody] CompleteUploadRequest request)
        {
            var safeFileName = Path.GetFileName(request.FileName);
            var chunkDir = Path.Combine(_basePath, "chunks", request.UploadId);

            var rawDir = Path.Combine(_basePath, "raw");
            Directory.CreateDirectory(rawDir);

            var finalPath = Path.Combine(rawDir, $"{Guid.NewGuid()}_{safeFileName}");

            var chunks = Directory.GetFiles(chunkDir)
                .Where(f => f.EndsWith(".part"))
                .OrderBy(f =>
                {
                    var name = Path.GetFileNameWithoutExtension(f);
                    return int.Parse(name);
                })
                .ToList();

            await using var finalStream = new FileStream(finalPath, FileMode.Create);

            foreach (var chunk in chunks)
            {
                await using var chunkStream = new FileStream(chunk, FileMode.Open);
                await chunkStream.CopyToAsync(finalStream);
            }

            Directory.Delete(chunkDir, true);

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            await _movieService.SaveMetadataAsync(userId, request.Title, finalPath);

            return Ok();
        }

        [Authorize]
        [HttpGet("users-movies")]
        public async Task<IActionResult> GetUsersMovies()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            int userId = int.Parse(userIdClaim);
            var movies = await _movieService.GetMoviesByUserId(userId);

            return Ok(new { items = movies });
        }

        [Authorize]
        [HttpGet("get-movie")]
        public async Task<IActionResult> GetMovie([FromQuery] int movieId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            int userId = int.Parse(userIdClaim);

            var movie = await _movieService.GetMovieById(movieId);

            return Ok(new { items = movie });
        }
    }
}
