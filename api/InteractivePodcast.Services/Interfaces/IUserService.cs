using InteractivePodcast.Shared.Models;

namespace InteractivePodcast.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> RegisterUser(UserDto userDto);
        Task<UserResponse> LoginUser(UserDto userDto);
    }
}
