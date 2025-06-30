using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyflixAPI.Models;

namespace MyflixAPI.Services
{
    public class ActorTranslationService
    {
        private readonly MyflixContext _context;

        public ActorTranslationService(MyflixContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> AddActorTranslation(ActorTranslation model)
        {
            if (model == null)
                return new BadRequestObjectResult("ActorTranslation data is null.");

            await _context.ActorTranslations.AddAsync(model);
            await _context.SaveChangesAsync();

            return new OkObjectResult(model);
        }

        public async Task<ActionResult> RemoveActorTranslation(int id)
        {
            var model = await _context.ActorTranslations.FindAsync(id);

            if (model == null)
                return new NotFoundObjectResult($"ActorTranslation is not found.");

            _context.ActorTranslations.Remove(model);
            await _context.SaveChangesAsync();

            return new OkResult();
        }

        public async Task<ActorTranslation?> SearchActorTranslationByIdAsync(int id)
        {
            return await _context.ActorTranslations.FindAsync(id);
        }

        public async Task<ActorTranslation?> SearchActorTranslationByNameAsync(string name, string language)
        {
            return await _context.ActorTranslations
                .Where(a => a.AtName == name && a.AtLanguage == language)
                .FirstOrDefaultAsync();
        }

        public async Task<List<ActorTranslation>> GetAllActorTranslationsAsync()
        {
            return await _context.ActorTranslations.ToListAsync();
        }
    }
}
