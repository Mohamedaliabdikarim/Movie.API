using Microsoft.AspNetCore.Mvc;
using Movie.Contracts.Services;
using Movie.Core.Dtos;

namespace Movie.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ActorsController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public ActorsController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ActorDto>>> GetActors()
    {
        var actors = await _serviceManager.ActorService.GetAllAsync();
        return Ok(actors);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ActorDto>> GetActor(int id)
    {
        var actor = await _serviceManager.ActorService.GetByIdAsync(id);

        if (actor is null)
        {
            return NotFound();
        }

        return Ok(actor);
    }

    [HttpPost]
    public async Task<ActionResult<ActorDto>> CreateActor(CreateActorDto dto)
    {
        var actor = await _serviceManager.ActorService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetActor),
            new { id = actor.Id },
            actor);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateActor(int id, CreateActorDto dto)
    {
        var updated = await _serviceManager.ActorService.UpdateAsync(id, dto);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteActor(int id)
    {
        var deleted = await _serviceManager.ActorService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}