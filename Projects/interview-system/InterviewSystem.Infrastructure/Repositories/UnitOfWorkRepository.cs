using InterviewSystem.Domain.Interfaces.Repositories;

namespace InterviewSystem.Infrastructure.Repositories;

public class UnitOfWorkRepository : IUnitOfWorkRepository
{
    private readonly AppDbContext _context;
    public UnitOfWorkRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync()
    {
       await _context.SaveChangesAsync();
    }
}
