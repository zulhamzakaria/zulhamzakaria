namespace ProcurementSystem.API.SharedKernel;

public abstract class BaseEntity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public Guid CreatedBy { get; protected set; }
    public DateTimeOffset CreatedAt { get; protected set; } 
    public Guid? UpdatedBy { get; protected set; }
    public DateTimeOffset? UpdatedAt { get; protected set; }

    protected BaseEntity()
        => CreatedAt = DateTimeOffset.UtcNow;

    //protected BaseEntity(Guid createdBy)
    //{
    //    CreatedBy = createdBy;
    //    CreatedAt = DateTimeOffset.UtcNow;
    //}

    public void SetUpdated(Guid updatedBy)
    {
        UpdatedBy = updatedBy;
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}
