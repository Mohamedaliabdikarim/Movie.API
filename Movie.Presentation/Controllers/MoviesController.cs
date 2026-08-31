using Microsoft.AspNetCore.Mvc;
using Movie.Contracts.Services;
using Movie.Core.Dtos;
using System.Runtime.InteropServices;

namespace Movie.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public MoviesController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies()
    {
        var movies = await _serviceManager.MovieService.GetAllAsync();
        return Ok(movies);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MovieDto>> GetMovie(int id)
    {
        var movie = await _serviceManager.MovieService.GetByIdAsync(id);

        if (movie is null)
        {
            return NotFound();
        }

        return Ok(movie);
    }

    [HttpGet("stats")]
    public async Task<ActionResult<MovieStatsDto>> GetStats()
    {
        var stats = await _serviceManager.MovieService.GetStatsAsync();
        return Ok(stats);
    }

    [HttpPost]
    public async Task<ActionResult<MovieDto>> CreateMovie(CreateMovieDto dto)
    {
        var movie = await _serviceManager.MovieService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetMovie),
            new { id = movie.Id },
            movie);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateMovie(int id, CreateMovieDto dto)
    {
        var updated = await _serviceManager.MovieService.UpdateAsync(id, dto);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        var deleted = await _serviceManager.MovieService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}