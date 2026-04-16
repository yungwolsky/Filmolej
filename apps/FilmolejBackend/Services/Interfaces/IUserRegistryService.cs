namespace FilmolejBackend.Services.Interfaces
{
    public interface IUserRegistryService
    {
        Task<bool> RegisterUserAsync(string username, string email, string password);
    }
}
