using Microsoft.AspNetCore.Mvc;

namespace RateLimiting.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Request successful");
        }
    }
}
