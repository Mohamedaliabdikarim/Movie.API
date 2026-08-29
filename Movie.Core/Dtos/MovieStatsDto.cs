namespace Movie.Core.Dtos;

public class MovieStatsDto
{
    public int TotalMovies { get; set; }
    public double AverageRating { get; set; }
    public double AverageDurationMinutes { get; set; }
    public int OldestReleaseYear { get; set; }
    public int NewestReleaseYear { get; set; }
}