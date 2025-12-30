using InterviewSystem.Domain.Common.Enums;
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

    private static readonly HashSet<InterviewTaskStatus> ActiveStatuses =
        [InterviewTaskStatus.Assigned,InterviewTaskStatus.Accepted];
    public async Task<IReadOnlyCollection<InterviewTask>> GetAllByEmployeeIdAsync(Guid employeeId)
    {

        return await _context.InterviewTasks
             .Where(it => it.AssigneeId == employeeId)
             .Where(it => ActiveStatuses.Contains(it.InterviewTaskStatus))
             .ToListAsync();
    }

    public async Task<InterviewTask?> GetByIdAsync(Guid id)
    {
        return await _context.InterviewTasks.FindAsync(id);
    }

    public async Task<bool> IsAssigneeOwnerAsync(Guid taskId, Guid assigneeId)
    {
        return await _context.InterviewTasks
            .AnyAsync(it => it.AssigneeId == assigneeId && it.Id == taskId);
    }
}
