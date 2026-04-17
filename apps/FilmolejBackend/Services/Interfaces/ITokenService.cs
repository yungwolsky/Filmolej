using FilmolejBackend.Models;

namespace FilmolejBackend.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
