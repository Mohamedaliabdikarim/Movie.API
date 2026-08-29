namespace Movie.Core.Dtos;

public class ReviewDto
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public double Rating { get; set; }
    public string? Comment { get; set; }
}