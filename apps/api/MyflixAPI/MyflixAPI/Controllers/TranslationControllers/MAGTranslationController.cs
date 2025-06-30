using Microsoft.AspNetCore.Mvc;
using MyflixAPI.TmdbServices;

namespace MyflixAPI.Controllers.TranslationControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MAGTranslationController : ControllerBase
    {
        private readonly MAGTranslationService _magTranslationService;

        public MAGTranslationController(MAGTranslationService magTranslationService)
        {
            _magTranslationService = magTranslationService;
        }

        [HttpGet]
        public IActionResult Hello() => Ok("MA - Translation Hello");

        [HttpGet("sync/movie")]
        public async Task<IActionResult> SyncMovieTranslation()
        {
            await _magTranslationService.MovieTranslation();
            return Ok("Sync is successful");
        }

        [HttpGet("sync/actor")]
        public async Task<IActionResult> SyncActorTranslation()
        {
            await _magTranslationService.ActorTranslation();
            return Ok("Sync is successful");
        }

        [HttpGet("sync/genre")]
        public async Task<IActionResult> SyncGenreTranslation()
        {
            await _magTranslationService.GenreTranslation();
            return Ok("Sync is successful");
        }
    }
}