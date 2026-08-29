using System.ComponentModel.DataAnnotations;

namespace Movie.Core.Entities;

public class Movie
{
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Genre { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Director { get; set; } = string.Empty;

    [Range(1888, 2100)]
    public int ReleaseYear { get; set; }

    [Range(1, int.MaxValue)]
    public int DurationMinutes { get; set; }

    [Range(0, 10)]
    public double Rating { get; set; }

    public string? Description { get; set; }

    public ICollection<Review> Reviews { get; set; } = new List<Review>();

    public ICollection<Actor> Actors { get; set; } = new List<Actor>();
}