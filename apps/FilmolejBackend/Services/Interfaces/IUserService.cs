using FilmolejBackend.Models;

namespace FilmolejBackend.Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<bool> AddUserAsync(string username, string email, string password);
    }
}
