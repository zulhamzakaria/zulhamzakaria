using InterviewSystem.Domain.Entity;

namespace InterviewSystem.Domain.Interfaces.Repositories;

public interface IInterviewTaskRepository
{
    Task<IReadOnlyCollection<InterviewTask>> GetAllAsync();
    Task<InterviewTask?> GetByIdAsync(Guid id);
    Task<IReadOnlyCollection<InterviewTask>> GetAllByEmployeeId(Guid employeeId);
    Task<bool> IsAssigneeOwner(Guid taskId, Guid assigneeId);
    Task AddAsync (InterviewTask task);
}
