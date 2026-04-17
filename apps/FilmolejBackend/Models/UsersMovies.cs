namespace FilmolejBackend.Models
{
    public class UsersMovies
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }

        public DateTime AddedAt { get; set; }
        public User User { get; set; }
        public Movie Movie { get; set; }
    }
}
