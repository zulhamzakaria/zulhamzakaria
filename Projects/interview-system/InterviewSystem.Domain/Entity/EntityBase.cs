namespace InterviewSystem.Domain.Entity;

public abstract class EntityBase
{
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    protected EntityBase()
    {
        CreatedAt = DateTimeOffset.UtcNow;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    protected void SetUpdated() => UpdatedAt = DateTimeOffset.UtcNow;
}
