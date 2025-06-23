using Microsoft.AspNetCore.Mvc;
using MyflixAPI.Models;
using MyflixAPI.Services;

namespace MyflixAPI.Controllers.DbControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly ActorService _service;

        public ActorController(ActorService service)
        {
            _service = service;
        }

        [HttpPost("add")]
        public async Task<ActionResult> Add([FromBody] Actor model) => await _service.AddActor(model);

        [HttpDelete("remove/{id}")]
        public async Task<ActionResult> Remove(int id) => await _service.RemoveActor(id);

        [HttpGet("search/id/{id}")]
        public async Task<ActionResult<Actor?>> SearchById(int id)
        {
            var result = await _service.SearchActorByIdAsync(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet("search/name/{name}")]
        public async Task<ActionResult<List<Actor>>> SearchByName(string name)
        {
            var result = await _service.SearchActorByNameAsync(name);
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<Actor>>> GetAll() => Ok(await _service.GetAllActorsAsync());
    }
}
