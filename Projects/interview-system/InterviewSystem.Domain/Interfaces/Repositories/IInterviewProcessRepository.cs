using InterviewSystem.Domain.Entity;

namespace InterviewSystem.Domain.Interfaces.Repositories;

public interface IInterviewProcessRepository
{
    Task<IReadOnlyCollection<InterviewProcess>> GetAllAsync();
    Task<InterviewProcess?> GetByIdAsync(Guid id);
    Task<InterviewProcess?> GetByCandidateIdAsync(Guid candidateId);
    Task AddAsync(InterviewProcess process);
}
