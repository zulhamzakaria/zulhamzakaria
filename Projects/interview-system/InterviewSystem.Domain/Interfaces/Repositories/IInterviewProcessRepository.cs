using InterviewSystem.Domain.Entity;

namespace InterviewSystem.Domain.Interfaces.Repositories;

public interface IInterviewProcessRepository
{
    Task<IReadOnlyCollection<InterviewProcess>> GetAllAsync();
    Task<InterviewProcess?> GetByIdAsync(Guid id);
    Task AddAsync(InterviewProcess process);
}
