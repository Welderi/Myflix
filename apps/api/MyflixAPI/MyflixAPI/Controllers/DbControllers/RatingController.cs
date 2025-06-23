using Microsoft.AspNetCore.Mvc;
using MyflixAPI.Models;
using MyflixAPI.Services;

namespace MyflixAPI.Controllers.DbControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly RatingService _service;

        public RatingController(RatingService service)
        {
            _service = service;
        }

        [HttpPost("add")]
        public async Task<ActionResult> Add([FromBody] Rating model) => await _service.AddRating(model);

        [HttpDelete("remove/usermovie/{userId}/{movieId}")]
        public async Task<ActionResult> Remove(string userId, int movieId)
            => await _service.RemoveRating(userId, movieId);

        [HttpGet("search/user/{userId}")]
        public async Task<ActionResult<List<Rating>>> SearchByUserId(string userId)
        {
            var result = await _service.SearchRatingByUserIdAsync(userId);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet("search/movie/{movieId}")]
        public async Task<ActionResult<List<Rating>>> SearchByMovieId(int movieId)
        {
            var result = await _service.SearchRatingByMovieIdAsync(movieId);
            return result != null ? Ok(result) : NotFound();
        }
    }
}
