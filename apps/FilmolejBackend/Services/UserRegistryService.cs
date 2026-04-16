using FilmolejBackend.Data;
using FilmolejBackend.Services.Interfaces;
using FilmolejBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FilmolejBackend.Services
{
    public class UserRegistryService(FilmolejDbContext db
        , ILogger<UserRegistryService> logger) : IUserRegistryService
    {
        private readonly FilmolejDbContext _db = db;
        private readonly DbSet<User> _users = db.Users;
        private readonly ILogger<UserRegistryService> _logger = logger;

        public async Task<bool> RegisterUserAsync(string username, string email
            , string password)
        {
            User NewUser = new User
            {
                Username = username,
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
            };

            try
            {
                await _users.AddAsync(NewUser);
                await _db.SaveChangesAsync();

                _logger.LogInformation("User created: {Email}", email);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Couldn't create user in database");
                return false;
            }
        }
    }
}
