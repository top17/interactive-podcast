using InteractivePodcast.Data.Interfaces;
using InteractivePodcast.Services.Interfaces;
using InteractivePodcast.Shared.Models;
using Microsoft.AspNetCore.Identity;

namespace InteractivePodcast.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ITokenService _tokenService;

        public UserService(
            IUserRepository userRepository,
            IPasswordHasher<User> passwordHasher,
            ITokenService tokenService
        )
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        public async Task<UserResponse> RegisterUser(UserDto userDto)
        {
            var userExists = await _userRepository.UserExists(userDto.Email);
            if (userExists)
            {
                return new UserResponse { Message = "User with this email already exists." };
            }

            var user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, userDto.Password);

            var userAdded = await _userRepository.AddUser(user);
            if (!userAdded)
            {
                return new UserResponse { Message = "Registration failed. Please try again." };
            }

            var token = GenerateToken(user);
            return CreateUserResponse(user, token, "User registered successfully.");
        }

        public async Task<UserResponse> LoginUser(UserDto userDto)
        {
            var user = await _userRepository.GetUserByEmail(userDto.Email);

            if (user == null)
                return new UserResponse { Message = "Invalid email or password." };

            var passwordCheck = _passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                userDto.Password
            );
            if (passwordCheck == PasswordVerificationResult.Failed)
                return new UserResponse { Message = "Invalid email or password." };

            var token = GenerateToken(user);
            return CreateUserResponse(user, token, "Login successful.");
        }

        private UserResponse CreateUserResponse(User user, string token, string message)
        {
            return new UserResponse
            {
                User = new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                },
                Token = token,
                Message = message
            };
        }

        private string GenerateToken(User user)
        {
            return _tokenService.CreateToken(user);
        }
    }
}
