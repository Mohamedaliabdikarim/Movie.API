using Microsoft.EntityFrameworkCore;
using Movie.Core.Contracts;
using Movie.Data.Data;

namespace Movie.Data.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly MovieContext _context;

    public MovieRepository(MovieContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Movie.Core.Entities.Movie>> GetAllAsync()
    {
        return await _context.Movies
            .Include(m => m.Actors)
            .Include(m => m.Reviews)
            .ToListAsync();
    }

    public async Task<Movie.Core.Entities.Movie?> GetByIdAsync(int id)
    {
        return await _context.Movies
            .Include(m => m.Actors)
            .Include(m => m.Reviews)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Movies.AnyAsync(m => m.Id == id);
    }

    public async Task<bool> ExistsByTitleAsync(string title)
    {
        return await _context.Movies
            .AnyAsync(m => m.Title.ToLower() == title.ToLower());
    }

    public async Task AddAsync(Movie.Core.Entities.Movie movie)
    {
        await _context.Movies.AddAsync(movie);
    }

    public void Update(Movie.Core.Entities.Movie movie)
    {
        _context.Movies.Update(movie);
    }

    public void Delete(Movie.Core.Entities.Movie movie)
    {
        _context.Movies.Remove(movie);
    }
}