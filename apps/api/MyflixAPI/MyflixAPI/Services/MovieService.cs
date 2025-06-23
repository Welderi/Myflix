using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyflixAPI.Models;

namespace MyflixAPI.Services
{
    public class MovieService
    {
        private readonly MyflixContext _context;

        public MovieService(MyflixContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> AddMovie(Movie model)
        {
            if (model == null)
                return new BadRequestObjectResult("Movie data is null.");

            await _context.Movies.AddAsync(model);
            await _context.SaveChangesAsync();

            return new OkObjectResult(model);
        }

        public async Task<ActionResult> RemoveMovie(int id)
        {
            var model = await _context.Movies.FindAsync(id);

            if (model == null)
                return new NotFoundObjectResult($"Movie is not found.");

            _context.Movies.Remove(model);
            await _context.SaveChangesAsync();

            return new OkResult();
        }

        public async Task<Movie?> SearchMovieByIdAsync(int id)
        {
            return await _context.Movies.FindAsync(id);
        }

        public async Task<Movie?> SearchMovieByNameAsync(string name)
        {
            return await _context.Movies
                .FirstOrDefaultAsync(m => m.MovieTitle == name);
        }

        public async Task<List<Movie>> GetAllMoviesAsync()
        {
            return await _context.Movies.ToListAsync();
        }
    }
}
