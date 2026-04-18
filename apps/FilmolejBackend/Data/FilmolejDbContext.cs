using Microsoft.EntityFrameworkCore;
using FilmolejBackend.Models;

namespace FilmolejBackend.Data
{
    public class FilmolejDbContext : DbContext
    {
        public FilmolejDbContext(DbContextOptions<FilmolejDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Movie>().ToTable("movies");
        }
    }
}
