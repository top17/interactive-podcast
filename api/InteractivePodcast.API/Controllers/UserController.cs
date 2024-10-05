using InteractivePodcast.Services.Interfaces;
using InteractivePodcast.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace InteractivePodcast.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            var response = await _userService.RegisterUser(userDto);

            if (response.User == null)
            {
                return BadRequest(new { Message = response.Message });
            }

            return Ok(
                new
                {
                    Token = response.Token,
                    UserId = response.User.Id,
                    Message = response.Message
                }
            );
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDto user)
        {
            var loginResponse = await _userService.LoginUser(user);

            if (loginResponse.User == null)
                return Unauthorized(loginResponse.Message);

            return Ok(
                new
                {
                    Token = loginResponse.Token,
                    User = loginResponse.User,
                    Message = loginResponse.Message
                }
            );
        }
    }
}
