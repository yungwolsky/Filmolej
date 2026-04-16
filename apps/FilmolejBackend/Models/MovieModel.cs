namespace FilmolejBackend.Models
{
    public class MovieModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string? PosterUrl { get; set; }
        public int? durationg { get; set; } 
    }
}
