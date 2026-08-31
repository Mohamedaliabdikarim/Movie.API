using Movie.Contracts.Services;
using Movie.Core.Contracts;
using Movie.Core.Dtos;
using Movie.Core.Entities;

namespace Movie.Services.Services;

public class ReviewService : IReviewService
{
    private readonly IUnitOfWork _unitOfWork;

    public ReviewService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ReviewDto>> GetByMovieIdAsync(int movieId)
    {
        var reviews = await _unitOfWork.Reviews.GetByMovieIdAsync(movieId);

        return reviews.Select(MapToDto);
    }

    public async Task<ReviewDto?> GetByIdAsync(int id)
    {
        var review = await _unitOfWork.Reviews.GetByIdAsync(id);

        return review is null ? null : MapToDto(review);
    }

    public async Task<ReviewDto> CreateAsync(int movieId, CreateReviewDto dto)
    {
        var movieExists = await _unitOfWork.Movies.ExistsAsync(movieId);
        if (!movieExists)
        {
            throw new InvalidOperationException($"Ingen film med id {movieId} finnes.");
        }

        var review = new Review
        {
            MovieId = movieId,
            Rating = dto.Rating,
            Comment = dto.Comment
        };

        await _unitOfWork.Reviews.AddAsync(review);
        await _unitOfWork.CompleteAsync();

        return MapToDto(review);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var review = await _unitOfWork.Reviews.GetByIdAsync(id);
        if (review is null)
        {
            return false;
        }

        _unitOfWork.Reviews.Delete(review);
        await _unitOfWork.CompleteAsync();

        return true;
    }

    private static ReviewDto MapToDto(Review review)
    {
        return new ReviewDto
        {
            Id = review.Id,
            MovieId = review.MovieId,
            Rating = review.Rating,
            Comment = review.Comment
        };
    }
}