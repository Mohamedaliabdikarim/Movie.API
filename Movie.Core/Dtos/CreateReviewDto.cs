using System.ComponentModel.DataAnnotations;

namespace Movie.Core.Dtos;

public class CreateReviewDto
{
    [Range(0, 10)]
    public double Rating { get; set; }

    public string? Comment { get; set; }
}