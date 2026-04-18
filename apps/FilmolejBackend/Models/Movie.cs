namespace FilmolejBackend.Models
{
    public static class MovieStatus
    {
        public const string Pending = "pending";
        public const string Processing = "processing";
        public const string Ready = "ready";
        public const string Failed = "failed";
    }

    public class Movie
    {
        public int Id { get; set; }

        // Movie info
        public string Title { get; set; } = string.Empty;
        public string Plot { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;
        public string ReleaseDate { get; set; }
        public string Genre { get; set; } = string.Empty;
        public string? PosterUrl { get; set; }
        public int UserId { get; set; }

        // File info
        public string OriginalFilePath { get; set; } = string.Empty;
        public string StreamPath { get; set; } = string.Empty;

        // Processing state
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}   
