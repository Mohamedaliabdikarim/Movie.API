using Movie.Core.Entities;

namespace Movie.Core.Contracts;

public interface IReviewRepository
{
    Task<IEnumerable<Review>> GetAllAsync();
    Task<IEnumerable<Review>> GetByMovieIdAsync(int movieId);
    Task<Review?> GetByIdAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task AddAsync(Review review);
    void Update(Review review);
    void Delete(Review review);
}