using Microsoft.AspNetCore.Mvc;
using MyflixAPI.DTOs;
using MyflixAPI.Services;

namespace MyflixAPI.Controllers.DbControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;

        public UserController(UserService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _service.Register(model);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _service.Login(model);
        }
    }
}
