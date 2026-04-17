using FilmolejBackend.Data;
using FilmolejBackend.Services.Interfaces;
using FilmolejBackend.Models;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FilmolejBackend.Services
{
    public class UserService(FilmolejDbContext db
        , ILogger<UserService> logger) : IUserService
    {
        private readonly FilmolejDbContext _db = db;
        private readonly DbSet<User> _users = db.Users;
        private readonly ILogger<UserService> _logger = logger;

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _users.SingleOrDefaultAsync(u => u.Email == email);
        }
        
        public async Task<User?> AddUserAsync(string username, string email
            , string password)
        {
            if(await _users.AnyAsync(u => u.Email == email))
            {
                return null;
            }

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
                return NewUser;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Couldn't create user in database");
                return null;
            }
        }
    }
}
