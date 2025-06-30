using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyflixAPI.Models;

namespace MyflixAPI.Services
{
    public class GenreTranslationService
    {
        private readonly MyflixContext _context;

        public GenreTranslationService(MyflixContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> AddGenreTranslation(GenreTranslation model)
        {
            if (model == null)
                return new BadRequestObjectResult("GenreTranslation data is null.");

            await _context.GenreTranslations.AddAsync(model);
            await _context.SaveChangesAsync();

            return new OkObjectResult(model);
        }

        public async Task<ActionResult> RemoveGenreTranslation(string name)
        {
            var model = await _context.GenreTranslations.FirstOrDefaultAsync(m => m.GtName == name);

            if (model == null)
                return new NotFoundObjectResult($"GenreTranslation is not found.");

            _context.GenreTranslations.Remove(model);
            await _context.SaveChangesAsync();

            return new OkResult();
        }

        public async Task<GenreTranslation?> SearchGenreTranslationByNameAsync(string name)
        {
            return await _context.GenreTranslations
                .Where(a => a.GtName == name)
                .FirstOrDefaultAsync();
        }


        public async Task<List<GenreTranslation>> GetAllGenreTranslationsAsync()
        {
            return await _context.GenreTranslations.ToListAsync();
        }
    }
}
