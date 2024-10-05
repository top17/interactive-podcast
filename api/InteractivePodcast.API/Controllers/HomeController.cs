using Microsoft.AspNetCore.Mvc;

namespace InteractivePodcast.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var data = new { Message = "Welcome to the Interactive Podcast API!" };
            return Ok(data);
        }
    }
}
