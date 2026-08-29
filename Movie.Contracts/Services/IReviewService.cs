using Movie.Core.Dtos;

namespace Movie.Contracts.Services;

public interface IReviewService
{
    Task<IEnumerable<ReviewDto>> GetByMovieIdAsync(int movieId);
    Task<ReviewDto?> GetByIdAsync(int id);
    Task<ReviewDto> CreateAsync(int movieId, CreateReviewDto dto);
    Task<bool> DeleteAsync(int id);
}