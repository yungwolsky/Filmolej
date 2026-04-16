using Microsoft.EntityFrameworkCore;
using FilmolejBackend.Models;

namespace FilmolejBackend.Data
{
    public class FilmolejDbContext : DbContext
    {
        public FilmolejDbContext(DbContextOptions<FilmolejDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<UsersMovies> UsersMovies { get; set; }
    }
}
