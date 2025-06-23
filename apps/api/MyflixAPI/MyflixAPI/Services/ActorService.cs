using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyflixAPI.Models;

namespace MyflixAPI.Services
{
    public class ActorService
    {
        private readonly MyflixContext _context;

        public ActorService(MyflixContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> AddActor(Actor model)
        {
            if (model == null)
                return new BadRequestObjectResult("Actor data is null.");

            await _context.Actors.AddAsync(model);
            await _context.SaveChangesAsync();

            return new OkObjectResult(model);
        }

        public async Task<ActionResult> RemoveActor(int id)
        {
            var model = await _context.Actors.FindAsync(id);

            if (model == null)
                return new NotFoundObjectResult($"Actor is not found.");

            _context.Actors.Remove(model);
            await _context.SaveChangesAsync();

            return new OkResult();
        }

        public async Task<Actor?> SearchActorByIdAsync(int id)
        {
            return await _context.Actors.FindAsync(id);
        }

        public async Task<List<Actor>> SearchActorByNameAsync(string name)
        {
            return await _context.Actors
                .Where(m => m.ActorName == name).ToListAsync();
        }

        public async Task<List<Actor>> GetAllActorsAsync()
        {
            return await _context.Actors.ToListAsync();
        }
    }
}
