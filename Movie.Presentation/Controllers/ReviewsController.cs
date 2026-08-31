using Microsoft.AspNetCore.Mvc;
using Movie.Contracts.Services;
using Movie.Core.Dtos;

namespace Movie.Presentation.Controllers;

[Route("api/movies/{movieId:int}/reviews")]
[ApiController]
public class ReviewsController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public ReviewsController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviews(int movieId)
    {
        var reviews = await _serviceManager.ReviewService.GetByMovieIdAsync(movieId);
        return Ok(reviews);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ReviewDto>> GetReview(int movieId, int id)
    {
        var review = await _serviceManager.ReviewService.GetByIdAsync(id);

        if (review is null || review.MovieId != movieId)
        {
            return NotFound();
        }

        return Ok(review);
    }

    [HttpPost]
    public async Task<ActionResult<ReviewDto>> CreateReview(int movieId, CreateReviewDto dto)
    {
        var review = await _serviceManager.ReviewService.CreateAsync(movieId, dto);

        return CreatedAtAction(
            nameof(GetReview),
            new { movieId, id = review.Id },
            review);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteReview(int movieId, int id)
    {
        var deleted = await _serviceManager.ReviewService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}