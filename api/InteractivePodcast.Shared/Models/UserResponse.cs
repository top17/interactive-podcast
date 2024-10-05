namespace InteractivePodcast.Shared.Models
{
    public class UserResponse
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
    }
}
