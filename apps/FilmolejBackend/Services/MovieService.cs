using FilmolejBackend.Data;
using FilmolejBackend.Models;
using FilmolejBackend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace FilmolejBackend.Services
{
    public class OmdbMovieDto
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Plot { get; set; }
        public string Poster { get; set; }
        public string Response { get; set; }
    }

    public class MovieService(FilmolejDbContext db
        , ILogger<IMovieService> logger, HttpClient client
        , IConfiguration config) : IMovieService
    {
        private readonly FilmolejDbContext _db = db;
        private readonly DbSet<Movie> _movies = db.Movies;
        private readonly ILogger<IMovieService> _logger = logger;
        private readonly string _apiKey = config["Omdb:ApiKey"];
        private readonly HttpClient _client = client;
        public async Task<bool> SaveMetadataAsync(int userId, string title, string file)
        {
            var url = $"http://www.omdbapi.com/?apikey={_apiKey}&t={Uri.EscapeDataString(title)}";
            HttpResponseMessage response = await _client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"OMDb request failed: {response.StatusCode}");
                return false;
            }

            var json = await response.Content.ReadAsStringAsync();
            _logger.LogInformation("OMDb response: {json}", json);

            var movieData = JsonSerializer.Deserialize<OmdbMovieDto>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (movieData == null || movieData.Response == "False")
            {
                _logger.LogWarning($"Movie not found in OMDb: {title}");
                return false;
            }

            var movie = new Movie
            {
                Title = movieData.Title,
                Plot = movieData.Plot,
                Director = movieData.Director,
                ReleaseDate = movieData.Year,
                Genre = movieData.Genre,
                PosterUrl = movieData.Poster,
                Duration = movieData.Runtime,
                UserId = userId,
                OriginalFilePath = file,
                Status = MovieStatus.Processing
            };

            try
            {
                await _movies.AddAsync(movie);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to upload movie");
                return false;
            }
        }

        public async Task<List<Movie>> GetMoviesByUserId(int userId)
        {
            return await _movies.Where(m => m.UserId == userId)
                .ToListAsync();
        }

        public async Task<Movie?> GetMovieById(int movieId)
        {
            return await _movies.FirstAsync(m => m.Id == movieId);
        }
    }
}
