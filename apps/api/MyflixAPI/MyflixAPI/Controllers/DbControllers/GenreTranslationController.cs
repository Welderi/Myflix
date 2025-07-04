using Microsoft.AspNetCore.Mvc;
using MyflixAPI.Models;
using MyflixAPI.Services;

namespace MyflixAPI.Controllers.DbControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreTranslationController : ControllerBase
    {
        private readonly GenreTranslationService _service;

        public GenreTranslationController(GenreTranslationService service)
        {
            _service = service;
        }

        [HttpPost("add")]
        public async Task<ActionResult> Add([FromBody] GenreTranslation model) => await _service.AddGenreTranslation(model);

        [HttpDelete("remove/{name}")]
        public async Task<ActionResult> Remove(string name) => await _service.RemoveGenreTranslation(name);

        [HttpGet("all")]
        public async Task<ActionResult<List<GenreTranslation>>> GetAll() => Ok(await _service.GetAllGenreTranslationsAsync());
    }
}
