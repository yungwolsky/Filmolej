using FilmolejBackend.Models;

namespace FilmolejBackend.Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> AddUserAsync(string username, string email, string password);
    }
}
