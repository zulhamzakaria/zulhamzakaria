using InterviewSystem.Domain.Entity;

namespace InterviewSystem.Domain.Interfaces.Repositories;

internal interface IInterviewTask
{
    Task<IReadOnlyCollection<InterviewTask>> GetAllAsync();
    Task<InterviewTask?> GetByIdAsync(Guid id);
    Task AddAsync (InterviewTask task);
}
