using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InterviewSystem.Infrastructure.Repositories;

public class InterviewTaskRepository : IInterviewTaskRepository
{
    private readonly AppDbContext _context;
    public InterviewTaskRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(InterviewTask task)
    {
        await _context.InterviewTasks.AddAsync(task);
    }

    public async Task<IReadOnlyCollection<InterviewTask>> GetAllAsync()
    {
        return await _context.InterviewTasks.ToListAsync();
    }

    public async Task<IReadOnlyCollection<InterviewTask>> GetAllByEmployeeId(Guid employeeId)
    {
        return await _context.InterviewTasks
             .Where(it => it.AssigneeId == employeeId)
             .ToListAsync();
    }

    public async Task<InterviewTask?> GetByIdAsync(Guid id)
    {
        return await _context.InterviewTasks.FindAsync(id);
    }
}
