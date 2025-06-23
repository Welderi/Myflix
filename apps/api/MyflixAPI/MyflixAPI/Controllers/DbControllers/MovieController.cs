using Microsoft.AspNetCore.Mvc;
using MyflixAPI.Models;
using MyflixAPI.Services;

namespace MyflixAPI.Controllers.DbControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieService _service;

        public MovieController(MovieService service)
        {
            _service = service;
        }

        [HttpPost("add")]
        public async Task<ActionResult> Add([FromBody] Movie model) => await _service.AddMovie(model);

        [HttpDelete("remove/{id}")]
        public async Task<ActionResult> Remove(int id) => await _service.RemoveMovie(id);

        [HttpGet("search/id/{id}")]
        public async Task<ActionResult<Movie?>> SearchById(int id)
        {
            var result = await _service.SearchMovieByIdAsync(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet("search/name/{name}")]
        public async Task<ActionResult<List<Movie>>> SearchByName(string name)
        {
            var result = await _service.SearchMovieByNameAsync(name);
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<Movie>>> GetAll() => Ok(await _service.GetAllMoviesAsync());
    }
}
