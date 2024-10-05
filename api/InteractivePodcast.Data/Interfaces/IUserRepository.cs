namespace InteractivePodcast.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmail(string email);
        Task<bool> UserExists(string email);
        Task<bool> AddUser(User user);
    }
}
