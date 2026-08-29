using Movie.Core.Entities;

namespace Movie.Core.Contracts;

public interface IMovieRepository
{
    Task<IEnumerable<Movie.Core.Entities.Movie>> GetAllAsync();
    Task<Movie.Core.Entities.Movie?> GetByIdAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<bool> ExistsByTitleAsync(string title);
    Task AddAsync(Movie.Core.Entities.Movie movie);
    void Update(Movie.Core.Entities.Movie movie);
    void Delete(Movie.Core.Entities.Movie movie);
}