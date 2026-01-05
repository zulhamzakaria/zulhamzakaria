using InterviewSystem.Domain.Entity;

namespace InterviewSystem.Domain.Interfaces.Repositories;

public interface IInterviewTaskRepository
{
    Task<IReadOnlyCollection<InterviewTask>> GetAllAsync();
    Task<InterviewTask?> GetByIdAsync(Guid id);
    Task<IReadOnlyCollection<InterviewTask>> GetAllByEmployeeIdAsync(Guid employeeId);
    Task<IReadOnlyCollection<InterviewTask>> GetAllByProcessIdAndSequence(Guid processId, int sequence);
    Task<Guid> GetByProcessIdAndAssigneeId(Guid processId, Guid assigneeId);
    Task<bool> IsAssigneeOwnerAsync(Guid taskId, Guid assigneeId);
    Task AddAsync (InterviewTask task);
}

