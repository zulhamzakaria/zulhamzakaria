using InterviewSystem.Domain.Entity;

namespace InterviewSystem.Domain.Interfaces.Repositories;

public interface ICandidateRepository
{
    Task<IReadOnlyCollection<Candidate>> GetAllAsync();
    Task<Candidate?> GetByIdAsync(Guid id); //maybe null hence the nullable
    Task AddAsync(Candidate candidate);
}
