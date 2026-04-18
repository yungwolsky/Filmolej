using FilmolejBackend.Models;

namespace FilmolejBackend.Services.Interfaces
{
    public interface IMovieService
    {
        Task<bool> UploadFileAsync(int userId, string title, IFormFile file);
        Task<List<Movie>> GetMoviesByUserId(int userId);
    }
}
