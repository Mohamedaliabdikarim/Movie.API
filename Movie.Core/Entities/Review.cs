using System.ComponentModel.DataAnnotations;

namespace Movie.Core.Entities;

public class Review
{
    public int Id { get; set; }

    public int MovieId { get; set; }

    public Movie? Movie { get; set; }

    [Range(0, 10)]
    public double Rating { get; set; }

    public string? Comment { get; set; }
}