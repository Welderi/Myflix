using Microsoft.AspNetCore.Mvc;
using MyflixAPI.Models;
using MyflixAPI.Services;

namespace MyflixAPI.Controllers.DbControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly GenreService _service;

        public GenreController(GenreService service)
        {
            _service = service;
        }

        [HttpPost("add")]
        public async Task<ActionResult> Add([FromBody] Genre model) => await _service.AddGenre(model);

        [HttpDelete("remove/{name}")]
        public async Task<ActionResult> Remove(string name) => await _service.RemoveGenre(name);

        [HttpGet("all")]
        public async Task<ActionResult<List<Genre>>> GetAll() => Ok(await _service.GetAllGenresAsync());
    }
}
