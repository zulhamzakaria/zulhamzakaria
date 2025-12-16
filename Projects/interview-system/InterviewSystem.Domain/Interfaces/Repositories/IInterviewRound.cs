using InterviewSystem.Domain.Entity;

namespace InterviewSystem.Domain.Interfaces.Repositories;

public interface IInterviewRound
{
    Task<IReadOnlyCollection<InterviewRound>> GetAllAsync();
    Task<InterviewRound?> GetByIdAsync(Guid id);
    Task AddAsync(InterviewRound round);
}
