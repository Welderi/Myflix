using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyflixAPI.Models;

namespace MyflixAPI.Services
{
    public class GenreService
    {
        private readonly MyflixContext _context;

        public GenreService(MyflixContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> AddGenre(Genre model)
        {
            if (model == null)
                return new BadRequestObjectResult("Genre data is null.");

            await _context.Genres.AddAsync(model);
            await _context.SaveChangesAsync();

            return new OkObjectResult(model);
        }

        public async Task<ActionResult> RemoveGenre(string name)
        {
            var model = await _context.Genres.FirstOrDefaultAsync(m => m.GenreName == name);

            if (model == null)
                return new NotFoundObjectResult($"Genre is not found.");

            _context.Genres.Remove(model);
            await _context.SaveChangesAsync();

            return new OkResult();
        }

        public async Task<List<Genre>> SearchGenreByNameAsync(string name)
        {
            return await _context.Genres
                .Where(m => m.GenreName == name).ToListAsync();
        }

        public async Task<List<Genre>> GetAllGenresAsync()
        {
            return await _context.Genres.ToListAsync();
        }
    }
}
