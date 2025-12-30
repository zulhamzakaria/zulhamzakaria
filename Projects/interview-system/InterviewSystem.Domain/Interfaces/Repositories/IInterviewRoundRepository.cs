using InterviewSystem.Domain.Entity;

namespace InterviewSystem.Domain.Interfaces.Repositories;

public interface IInterviewRoundRepository
{
    Task<IReadOnlyCollection<InterviewRound>> GetAllAsync();
    Task<InterviewRound?> GetByIdAsync(Guid id);
    Task<InterviewRoundItem?> GetNextSequenceAsync(Guid roundId, int currentSequence);
    Task<InterviewRoundItem?> GetCurrentSequenceAsync(Guid roundId, int currentSequence);
    Task AddAsync(InterviewRound round);
}
