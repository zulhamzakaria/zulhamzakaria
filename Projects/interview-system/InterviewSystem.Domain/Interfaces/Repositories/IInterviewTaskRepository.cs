using InterviewSystem.Domain.Entity;

namespace InterviewSystem.Domain.Interfaces.Repositories;

public interface IInterviewTaskRepository
{
    Task<IReadOnlyCollection<InterviewTask>> GetAllAsync();
    Task<InterviewTask?> GetByIdAsync(Guid id);
    Task<IReadOnlyCollection<InterviewTask>> GetAllByEmployeeId(Guid employeeId);
    Task AddAsync (InterviewTask task);
}
