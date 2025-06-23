using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyflixAPI.Models;

namespace MyflixAPI.Services
{
    public class ReviewService
    {
        private readonly MyflixContext _context;

        public ReviewService(MyflixContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> AddReview(Review model)
        {
            if (model == null)
                return new BadRequestObjectResult("Review data is null.");

            await _context.Reviews.AddAsync(model);
            await _context.SaveChangesAsync();

            return new OkObjectResult(model);
        }

        public async Task<ActionResult> RemoveReview(int id)
        {
            var model = await _context.Reviews.FindAsync(id);

            if (model == null)
                return new NotFoundObjectResult($"Review is not found.");

            _context.Reviews.Remove(model);
            await _context.SaveChangesAsync();

            return new OkResult();
        }

        public async Task<ActionResult> RemoveReviewByUserMovie(string userId, int movieId)
        {
            var model = await _context.Reviews
                .FirstOrDefaultAsync(m => m.ReviewUserIdRef == userId && m.ReviewMovieIdRef == movieId);

            if (model == null)
                return new NotFoundObjectResult($"Review is not found.");

            _context.Reviews.Remove(model);
            await _context.SaveChangesAsync();

            return new OkResult();
        }

        public async Task<List<Review>> GetAllReviewsAsync()
        {
            return await _context.Reviews.ToListAsync();
        }

        public async Task<List<Review>> GetAllReviewsByMovieAsync(int movieId)
        {
            return await _context.Reviews.Where(m => m.ReviewMovieIdRef == movieId).ToListAsync();
        }
    }
}
