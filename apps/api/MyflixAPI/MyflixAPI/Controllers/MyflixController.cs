using Microsoft.AspNetCore.Mvc;

namespace MyflixAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MyflixController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("hello");
        }
    }
}
