using Microsoft.EntityFrameworkCore;
using FilmolejBackend.Models;

namespace FilmolejBackend.Data
{
    public class FilmolejDbContext : DbContext
    {
        public FilmolejDbContext(DbContextOptions<FilmolejDbContext> options) : base(options) { }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<MovieModel> Movies { get; set; }
        public DbSet<UsersMoviesModel> UsersMovies { get; set; }
    }
}
