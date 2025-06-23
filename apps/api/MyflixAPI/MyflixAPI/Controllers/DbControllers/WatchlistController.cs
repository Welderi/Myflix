using Microsoft.AspNetCore.Mvc;
using MyflixAPI.Models;
using MyflixAPI.Services;

namespace MyflixAPI.Controllers.DbControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatchlistController : ControllerBase
    {
        private readonly WatchlistService _service;

        public WatchlistController(WatchlistService service)
        {
            _service = service;
        }

        [HttpPost("add")]
        public async Task<ActionResult> Add([FromBody] Watchlist model) => await _service.AddWatchlist(model);

        [HttpDelete("remove/{userId}/{movieId}")]
        public async Task<ActionResult> RemoveByUserMovie(string userId, int movieId)
            => await _service.RemoveWatchlistByUserMovie(userId, movieId);

        [HttpGet("search/userid/{userId}")]
        public async Task<ActionResult<List<Watchlist>>> SearchById(string userId)
        {
            var result = await _service.SearchWatchlistByUserIdAsync(userId);
            return result != null ? Ok(result) : NotFound();
        }
    }
}
