using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InterviewSystem.Infrastructure.Repositories;

public class CandidateRepository : ICandidateRepository
{
    private readonly AppDbContext _context;
    public CandidateRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(Candidate candidate)
    {
        await _context.Candidates.AddAsync(candidate);
    }

    public async Task<IReadOnlyCollection<Candidate>> GetAllAsync()
    {
        return await _context.Candidates.ToListAsync();
    }

    public async Task<Candidate?> GetByIdAsync(Guid id)
    {
        return await _context.Candidates.FindAsync(id);
    }
}
