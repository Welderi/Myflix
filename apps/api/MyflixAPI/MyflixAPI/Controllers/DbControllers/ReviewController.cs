using Microsoft.AspNetCore.Mvc;
using MyflixAPI.Models;
using MyflixAPI.Services;

namespace MyflixAPI.Controllers.DbControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewService _service;

        public ReviewController(ReviewService service)
        {
            _service = service;
        }

        [HttpPost("add")]
        public async Task<ActionResult> Add([FromBody] Review model) => await _service.AddReview(model);

        [HttpDelete("remove/{id}")]
        public async Task<ActionResult> Remove(int id) => await _service.RemoveReview(id);

        [HttpDelete("remove/usermovie/{userId}/{movieId}")]
        public async Task<ActionResult> RemoveByUserMovie(string userId, int movieId)
            => await _service.RemoveReviewByUserMovie(userId, movieId);

        [HttpGet("all")]
        public async Task<ActionResult<List<Review>>> GetAll() => Ok(await _service.GetAllReviewsAsync());

        [HttpGet("all/movie/{movieId}")]
        public async Task<ActionResult<List<Review>>> GetAllMovie(int movieId)
            => Ok(await _service.GetAllReviewsByMovieAsync(movieId));
    }
}
