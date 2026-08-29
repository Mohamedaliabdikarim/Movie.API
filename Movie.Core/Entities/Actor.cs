using System.ComponentModel.DataAnnotations;

namespace Movie.Core.Entities;

public class Actor
{
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string Name { get; set; } = string.Empty;

    public int? BornYear { get; set; }

    public string? Biography { get; set; }

    public ICollection<Movie> Movies { get; set; } = new List<Movie>();
}