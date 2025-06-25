using Microsoft.AspNetCore.Mvc;
using MyflixAPI.TmdbServices;

namespace MyflixAPI.Controllers.DbControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TmdbController : ControllerBase
    {
        private readonly TmdbService _service;
        public TmdbController(TmdbService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Hello() => Ok("Hello TMDB");

        [HttpGet("sync/genre")]
        public async Task<IActionResult> SyncGenre()
        {
            await _service.SyncGenresAsync();
            return Ok("Sync is successful");
        }

        [HttpGet("sync/movie")]
        public async Task<IActionResult> SyncMovie()
        {
            await _service.SyncMovieAsync();
            return Ok("Sync is successful");
        }

        [HttpGet("sync/actor")]
        public async Task<IActionResult> SyncActor()
        {
            await _service.SyncActorAsync();
            return Ok("Sync is successful");
        }
    }
}
