using Microsoft.AspNetCore.Mvc;
using MyflixAPI.Models;
using MyflixAPI.Services;

namespace MyflixAPI.Controllers.DbControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorTranslationController : ControllerBase
    {
        private readonly ActorTranslationService _service;

        public ActorTranslationController(ActorTranslationService service)
        {
            _service = service;
        }

        [HttpPost("add")]
        public async Task<ActionResult> Add([FromBody] ActorTranslation model) => await _service.AddActorTranslation(model);

        [HttpDelete("remove/{id}")]
        public async Task<ActionResult> Remove(int id) => await _service.RemoveActorTranslation(id);

        [HttpGet("search/id/{id}")]
        public async Task<ActionResult<ActorTranslation?>> SearchById(int id)
        {
            var result = await _service.SearchActorTranslationByIdAsync(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet("search/name/{name}")]
        public async Task<ActionResult<List<ActorTranslation>>> SearchByName(string name)
        {
            var result = await _service.SearchActorTranslationByNameAsync(name, "uk");
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<ActorTranslation>>> GetAll() => Ok(await _service.GetAllActorTranslationsAsync());
    }
}
