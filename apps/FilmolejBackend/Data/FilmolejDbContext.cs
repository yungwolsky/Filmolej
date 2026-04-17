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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Movie>().ToTable("movies");
            modelBuilder.Entity<UsersMovies>().ToTable("users_movies");

            modelBuilder.Entity<UsersMovies>()
                .HasKey(x => new { x.UserId, x.MovieId });
        }
    }
}
