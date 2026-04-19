using FilmolejBackend.Models;

namespace FilmolejBackend.Services.Interfaces
{
    public interface IMovieService
    {
        Task<bool> SaveMetadataAsync(int userId, string title, string file);
        Task<List<Movie>> GetMoviesByUserId(int userId);
        Task<Movie?> GetMovieById(int movieId);
    }
}
