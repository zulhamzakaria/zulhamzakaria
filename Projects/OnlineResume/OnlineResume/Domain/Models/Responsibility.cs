namespace Domain.Models;

public class Responsibility
{
    public int Id { get; set; }
    public int ExperienceId { get; set; }
    public string? Description { get; set; }
    public Experience? Experience { get; set; }
}
