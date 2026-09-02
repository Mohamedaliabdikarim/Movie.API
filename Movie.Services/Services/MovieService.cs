using Movie.Contracts.Services;
using Movie.Core.Contracts;
using Movie.Core.Dtos;
using Movie.Core.Exceptions;

namespace Movie.Services.Services;

public class MovieService : IMovieService
{
    private readonly IUnitOfWork _unitOfWork;

    public MovieService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<MovieDto>> GetAllAsync()
    {
        var movies = await _unitOfWork.Movies.GetAllAsync();

        return movies.Select(MapToDto);
    }

    public async Task<MovieDto?> GetByIdAsync(int id)
    {
        var movie = await _unitOfWork.Movies.GetByIdAsync(id);

        return movie is null ? null : MapToDto(movie);
    }

    public async Task<MovieDto> CreateAsync(CreateMovieDto dto)
    {
        var titleTaken = await _unitOfWork.Movies.ExistsByTitleAsync(dto.Title);
        if (titleTaken)
        {
            throw new BusinessRuleException($"A movie with the title '{dto.Title}' already exists.");
        }

        var movie = new Movie.Core.Entities.Movie
        {
            Title = dto.Title,
            Genre = dto.Genre,
            Director = dto.Director,
            ReleaseYear = dto.ReleaseYear,
            DurationMinutes = dto.DurationMinutes,
            Rating = dto.Rating,
            Description = dto.Description
        };

        await _unitOfWork.Movies.AddAsync(movie);
        await _unitOfWork.CompleteAsync();

        return MapToDto(movie);
    }

    public async Task<bool> UpdateAsync(int id, CreateMovieDto dto)
    {
        var movie = await _unitOfWork.Movies.GetByIdAsync(id);
        if (movie is null)
        {
            return false;
        }

        var titleTakenByOther = (await _unitOfWork.Movies.GetAllAsync())
            .Any(m => m.Id != id && m.Title.Equals(dto.Title, StringComparison.CurrentCultureIgnoreCase));

        if (titleTakenByOther)
        {
            throw new BusinessRuleException($"A movie with the title '{dto.Title}' already exists.");
        }

        movie.Title = dto.Title;
        movie.Genre = dto.Genre;
        movie.Director = dto.Director;
        movie.ReleaseYear = dto.ReleaseYear;
        movie.DurationMinutes = dto.DurationMinutes;
        movie.Rating = dto.Rating;
        movie.Description = dto.Description;

        _unitOfWork.Movies.Update(movie);
        await _unitOfWork.CompleteAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var movie = await _unitOfWork.Movies.GetByIdAsync(id);
        if (movie is null)
        {
            return false;
        }

        _unitOfWork.Movies.Delete(movie);
        await _unitOfWork.CompleteAsync();

        return true;
    }

    public async Task<MovieStatsDto> GetStatsAsync()
    {
        var movies = (await _unitOfWork.Movies.GetAllAsync()).ToList();

        if (movies.Count == 0)
        {
            return new MovieStatsDto
            {
                TotalMovies = 0,
                AverageRating = 0,
                AverageDurationMinutes = 0,
                OldestReleaseYear = 0,
                NewestReleaseYear = 0
            };
        }

        return new MovieStatsDto
        {
            TotalMovies = movies.Count,
            AverageRating = movies.Average(m => m.Rating),
            AverageDurationMinutes = movies.Average(m => m.DurationMinutes),
            OldestReleaseYear = movies.Min(m => m.ReleaseYear),
            NewestReleaseYear = movies.Max(m => m.ReleaseYear)
        };
    }

    private static MovieDto MapToDto(Movie.Core.Entities.Movie movie)
    {
        return new MovieDto
        {
            Id = movie.Id,
            Title = movie.Title,
            Genre = movie.Genre,
            Director = movie.Director,
            ReleaseYear = movie.ReleaseYear,
            DurationMinutes = movie.DurationMinutes,
            Rating = movie.Rating,
            Description = movie.Description
        };
    }
}