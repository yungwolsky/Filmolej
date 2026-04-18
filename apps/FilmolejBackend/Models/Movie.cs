namespace FilmolejBackend.Models
{
    public enum MovieStatus
    {
        Uploaded, // file received
        Processing, // FFmpged running
        Ready, // available for streaming
        Failed // transcoding error
    }

    public class Movie
    {
        public int Id { get; set; }

        // Movie info
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; } = string.Empty;
        public string? PosterUrl { get; set; }

        // File info
        public string OriginalFilePath { get; set; } = string.Empty;
        public string StreamPath { get; set; } = string.Empty;

        // Processing state
        public MovieStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}   
