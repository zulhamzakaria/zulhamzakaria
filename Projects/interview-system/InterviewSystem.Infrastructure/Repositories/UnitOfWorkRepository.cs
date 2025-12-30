using InterviewSystem.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InterviewSystem.Infrastructure.Repositories;

public class UnitOfWorkRepository : IUnitOfWorkRepository
{
    private readonly AppDbContext _context;
    public UnitOfWorkRepository(AppDbContext context)
    {
        _context = context;
    }

    public EntityState GetEntityState<TEntity>(TEntity entity) where TEntity : class
    {
        return _context.Entry(entity).State;
    }

    public async Task SaveChangesAsync()
    {
       await _context.SaveChangesAsync();
    }
}
