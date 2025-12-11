namespace InterviewSystem.Domain.Entity;

public abstract class EntityBase
{
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    protected EntityBase()
    {
        CreatedAt = DateTimeOffset.Now;
        UpdatedAt = DateTimeOffset.Now;
    }

    protected void SetUpdated() => UpdatedAt = DateTimeOffset.Now;
}
