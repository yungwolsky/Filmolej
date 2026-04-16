using System.Numerics;

namespace FilmolejBackend.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Userame { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
