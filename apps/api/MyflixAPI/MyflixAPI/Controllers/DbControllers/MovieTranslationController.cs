using Microsoft.AspNetCore.Mvc;
using MyflixAPI.Models;
using MyflixAPI.Services;

namespace MyflixAPI.Controllers.DbControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieTranslationController : ControllerBase
    {
        private readonly MovieTranslationService _service;

        public MovieTranslationController(MovieTranslationService service)
        {
            _service = service;
        }

        [HttpPost("add")]
        public async Task<ActionResult> Add([FromBody] MovieTranslation model) => await _service.AddMovieTranslation(model);

        [HttpDelete("remove/{id}")]
        public async Task<ActionResult> Remove(int id) => await _service.RemoveMovieTranslation(id);

        [HttpGet("search/id/{id}")]
        public async Task<ActionResult<MovieTranslation?>> SearchById(int id)
        {
            var result = await _service.SearchMovieTranslationByIdAsync(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet("search/name/{name}")]
        public async Task<ActionResult<List<MovieTranslation>>> SearchByName(string name)
        {
            var result = await _service.SearchMovieTranslationByNameAsync(name, "uk");
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<MovieTranslation>>> GetAll() => Ok(await _service.GetAllMovieTranslationsAsync());
    }
}
