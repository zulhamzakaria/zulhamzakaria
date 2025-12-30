using Microsoft.EntityFrameworkCore;

namespace InterviewSystem.Domain.Interfaces.Repositories;

public interface IUnitOfWorkRepository
{
    Task SaveChangesAsync();
    EntityState GetEntityState<TEntity>(TEntity entity) where TEntity : class;
}
