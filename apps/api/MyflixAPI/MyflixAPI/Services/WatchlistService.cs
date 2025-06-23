using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyflixAPI.Models;

namespace MyflixAPI.Services
{
    public class WatchlistService
    {
        private readonly MyflixContext _context;

        public WatchlistService(MyflixContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> AddWatchlist(Watchlist model)
        {
            if (model == null)
                return new BadRequestObjectResult("Watchlist data is null.");

            await _context.Watchlists.AddAsync(model);
            await _context.SaveChangesAsync();

            return new OkObjectResult(model);
        }

        public async Task<ActionResult> RemoveWatchlistByUserMovie(string userId, int movieId)
        {
            var model = await _context.Watchlists
                .FirstOrDefaultAsync(m => m.WatchlistUserIdRef == userId && m.WatchlistMovieIdRef == movieId);

            if (model == null)
                return new NotFoundObjectResult($"Watchlist is not found.");

            _context.Watchlists.Remove(model);
            await _context.SaveChangesAsync();

            return new OkResult();
        }

        public async Task<List<Watchlist>> SearchWatchlistByUserIdAsync(string userId)
        {
            return await _context.Watchlists
                .Where(m => m.WatchlistUserIdRef == userId).ToListAsync();
        }
    }
}

