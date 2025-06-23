using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyflixAPI.Models;

namespace MyflixAPI.Services
{
    public class RatingService
    {
        private readonly MyflixContext _context;

        public RatingService(MyflixContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> AddRating(Rating model)
        {
            if (model == null)
                return new BadRequestObjectResult("Rating data is null.");

            await _context.Ratings.AddAsync(model);
            await _context.SaveChangesAsync();

            return new OkObjectResult(model);
        }

        public async Task<ActionResult> RemoveRating(string userId, int movieId)
        {
            var model = await _context.Ratings
                .FirstOrDefaultAsync(m => m.RatingUserIdRef == userId && m.RatingMovieIdRef == movieId);

            if (model == null)
                return new NotFoundObjectResult("Rating not found.");

            _context.Ratings.Remove(model);
            await _context.SaveChangesAsync();

            return new OkResult();
        }

        public async Task<List<Rating>> SearchRatingByUserIdAsync(string id)
        {
            return await _context.Ratings.Where(m => m.RatingUserIdRef == id).ToListAsync();
        }

        public async Task<List<Rating>> SearchRatingByMovieIdAsync(int id)
        {
            return await _context.Ratings.Where(m => m.RatingMovieIdRef == id).ToListAsync();
        }
    }
}
