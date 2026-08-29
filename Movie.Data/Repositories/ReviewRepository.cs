using Microsoft.EntityFrameworkCore;
using Movie.Core.Contracts;
using Movie.Core.Entities;
using Movie.Data.Data;

namespace Movie.Data.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly MovieContext _context;

    public ReviewRepository(MovieContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Review>> GetAllAsync()
    {
        return await _context.Reviews.ToListAsync();
    }

    public async Task<IEnumerable<Review>> GetByMovieIdAsync(int movieId)
    {
        return await _context.Reviews
            .Where(r => r.MovieId == movieId)
            .ToListAsync();
    }

    public async Task<Review?> GetByIdAsync(int id)
    {
        return await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Reviews.AnyAsync(r => r.Id == id);
    }

    public async Task AddAsync(Review review)
    {
        await _context.Reviews.AddAsync(review);
    }

    public void Update(Review review)
    {
        _context.Reviews.Update(review);
    }

    public void Delete(Review review)
    {
        _context.Reviews.Remove(review);
    }
}