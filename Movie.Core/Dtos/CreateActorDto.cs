using System.ComponentModel.DataAnnotations;

namespace Movie.Core.Dtos;

public class CreateActorDto
{
    [Required]
    [StringLength(150)]
    public string Name { get; set; } = string.Empty;

    public int? BornYear { get; set; }

    public string? Biography { get; set; }
}