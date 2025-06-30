using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyflixAPI.Models;

namespace MyflixAPI.Services
{
    public class MovieTranslationService
    {
        private readonly MyflixContext _context;

        public MovieTranslationService(MyflixContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> AddMovieTranslation(MovieTranslation model)
        {
            if (model == null)
                return new BadRequestObjectResult("MovieTranslation data is null.");

            await _context.MovieTranslations.AddAsync(model);
            await _context.SaveChangesAsync();

            return new OkObjectResult(model);
        }

        public async Task<ActionResult> RemoveMovieTranslation(int id)
        {
            var model = await _context.MovieTranslations.FindAsync(id);

            if (model == null)
                return new NotFoundObjectResult($"MovieTranslation is not found.");

            _context.MovieTranslations.Remove(model);
            await _context.SaveChangesAsync();

            return new OkResult();
        }

        public async Task<MovieTranslation?> SearchMovieTranslationByIdAsync(int id)
        {
            return await _context.MovieTranslations.FindAsync(id);
        }

        public async Task<MovieTranslation?> SearchMovieTranslationByNameAsync(string title, string language)
        {
            return await _context.MovieTranslations
                .Where(a => a.MtTitle == title && a.MtLanguage == language)
                .FirstOrDefaultAsync();
        }

        public async Task<List<MovieTranslation>> GetAllMovieTranslationsAsync()
        {
            return await _context.MovieTranslations.ToListAsync();
        }
    }
}
