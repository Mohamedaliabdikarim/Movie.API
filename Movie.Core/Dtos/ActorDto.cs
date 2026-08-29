namespace Movie.Core.Dtos;

public class ActorDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int? BornYear { get; set; }
    public string? Biography { get; set; }
}