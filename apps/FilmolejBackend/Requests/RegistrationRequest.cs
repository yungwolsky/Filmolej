using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FilmolejBackend.Requests
{
    public sealed class RegistrationRequest
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
