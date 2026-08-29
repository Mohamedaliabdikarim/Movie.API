using Movie.Core.Dtos;

namespace Movie.Contracts.Services;

public interface IMovieService
{
    Task<IEnumerable<MovieDto>> GetAllAsync();
    Task<MovieDto?> GetByIdAsync(int id);
    Task<MovieDto> CreateAsync(CreateMovieDto dto);
    Task<bool> UpdateAsync(int id, CreateMovieDto dto);
    Task<bool> DeleteAsync(int id);
    Task<MovieStatsDto> GetStatsAsync();
}