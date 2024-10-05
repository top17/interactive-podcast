using InteractivePodcast.Data.Interfaces;
using InteractivePodcast.Services.Implementations;
using InteractivePodcast.Services.Interfaces;
using InteractivePodcast.Shared.Models;
using Microsoft.AspNetCore.Identity;

public class UserServiceTests
{
    private readonly UserService _userService;
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<IPasswordHasher<User>> _passwordHasherMock = new();
    private readonly Mock<ITokenService> _tokenServiceMock = new();

    public UserServiceTests()
    {
        _userService = new UserService(
            _userRepositoryMock.Object,
            _passwordHasherMock.Object,
            _tokenServiceMock.Object
        );
    }

    [Fact]
    public async Task RegisterUser_ShouldReturnNull_WhenEmailAlreadyExists()
    {
        _userRepositoryMock.Setup(repo => repo.UserExists(It.IsAny<string>())).ReturnsAsync(true);

        var registrationDto = new UserDto
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "existingemail@example.com",
            Password = "password"
        };

        var result = await _userService.RegisterUser(registrationDto);

        Assert.Null(result.User);
        Assert.Equal("User with this email already exists.", result.Message);
    }

    [Fact]
    public async Task RegisterUser_ShouldReturnUserAndToken_WhenRegistrationIsSuccessful()
    {
        _userRepositoryMock.Setup(repo => repo.UserExists(It.IsAny<string>())).ReturnsAsync(false);
        _userRepositoryMock.Setup(repo => repo.AddUser(It.IsAny<User>())).ReturnsAsync(true);
        _tokenServiceMock.Setup(ts => ts.CreateToken(It.IsAny<User>())).Returns("generatedToken");

        var registrationDto = new UserDto
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "newuser@example.com",
            Password = "password"
        };

        var result = await _userService.RegisterUser(registrationDto);

        Assert.NotNull(result.User);
        Assert.Equal("generatedToken", result.Token);
        Assert.Equal("User registered successfully.", result.Message);
    }

    [Fact]
    public async Task LoginUser_ShouldReturnNull_WhenUserDoesNotExist()
    {
        _userRepositoryMock
            .Setup(repo => repo.GetUserByEmail(It.IsAny<string>()))
            .ReturnsAsync((User)null);
        var loginDto = new UserDto { Email = "nonexistentuser@example.com", Password = "password" };

        var result = await _userService.LoginUser(loginDto);

        Assert.Null(result.User);
        Assert.Equal("Invalid email or password.", result.Message);
    }

    [Fact]
    public async Task LoginUser_ShouldReturnNull_WhenPasswordIsIncorrect()
    {
        var user = new User { Email = "existinguser@example.com", PasswordHash = "hashedpassword" };
        _userRepositoryMock
            .Setup(repo => repo.GetUserByEmail(It.IsAny<string>()))
            .ReturnsAsync(user);
        _passwordHasherMock
            .Setup(ph =>
                ph.VerifyHashedPassword(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>())
            )
            .Returns(PasswordVerificationResult.Failed);

        var loginDto = new UserDto
        {
            Email = "existinguser@example.com",
            Password = "wrongpassword"
        };

        var result = await _userService.LoginUser(loginDto);

        Assert.Null(result.User);
        Assert.Equal("Invalid email or password.", result.Message);
    }

    [Fact]
    public async Task LoginUser_ShouldReturnUserAndToken_WhenLoginIsSuccessful()
    {
        var user = new User { Email = "existinguser@example.com", PasswordHash = "hashedpassword" };
        _userRepositoryMock
            .Setup(repo => repo.GetUserByEmail(It.IsAny<string>()))
            .ReturnsAsync(user);
        _passwordHasherMock
            .Setup(ph =>
                ph.VerifyHashedPassword(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>())
            )
            .Returns(PasswordVerificationResult.Success);
        _tokenServiceMock.Setup(ts => ts.CreateToken(It.IsAny<User>())).Returns("validToken");

        var loginDto = new UserDto
        {
            Email = "existinguser@example.com",
            Password = "correctpassword"
        };

        var result = await _userService.LoginUser(loginDto);

        Assert.NotNull(result.User);
        Assert.Equal("validToken", result.Token);
        Assert.Equal("Login successful.", result.Message);
    }
}
