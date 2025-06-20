using Microsoft.AspNetCore.Mvc;

namespace MyflixAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MyflixController : ControllerBase
    {
        public IActionResult Index()
        {
            return Ok("hello");
        }
    }
}
