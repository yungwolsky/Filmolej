using System.ComponentModel.DataAnnotations;

namespace FilmolejBackend.Requests
{
    public class MovieUploadRequest
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public IFormFile? File { get; set; } = null;
    }
}
