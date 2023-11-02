namespace Domain.Models;

public class Experience
{
    public int Id { get; set; }
    public string? CompanyName { get; set; }
    public string? JobTitle { get; set; }
    public string? Period { get; set; }
    public List<Responsibility>? Responsibilities { get; set; }
}
