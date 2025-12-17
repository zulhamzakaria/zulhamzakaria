using InterviewSystem.Domain.Entity;

namespace InterviewSystem.Domain.Interfaces.Repositories;

public interface IInterviewRoundRepository
{
    Task<IReadOnlyCollection<InterviewRound>> GetAllAsync();
    Task<InterviewRound?> GetByIdAsync(Guid id);
    Task AddAsync(InterviewRound round);
}
