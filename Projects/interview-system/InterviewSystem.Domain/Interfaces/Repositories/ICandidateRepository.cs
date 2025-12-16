using InterviewSystem.Domain.Entity;

namespace InterviewSystem.Domain.Interfaces.Repositories;

public interface ICandidateRepository
{
    Task<IReadOnlyCollection<Candidate>> GetAllAsync(); //ToList always return an empty list
    Task<Candidate?> GetByIdAsync(Guid id); //maybe null hence the nullable
    Task AddAsync(Candidate candidate);
}
