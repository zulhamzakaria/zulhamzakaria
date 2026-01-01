using InterviewSystem.Application.Candidates.GetCandidatesSummary;
using InterviewSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace InterviewSystem.Infrastructure.CustomQueries.Candidates;

public class CandidatesSummaryQueryRepository : ICandidatesSummaryQueryRepository
{
    private readonly AppDbContext _dbContext;

    public CandidatesSummaryQueryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private IQueryable<Candidate> Query()
        => _dbContext.Set<Candidate>().AsNoTracking();

    public async Task<IReadOnlyCollection<GetCandidatesSummaryDTO>> GetSummaryAsync()
    {
        return await Query()
            .Select(candidate => new GetCandidatesSummaryDTO(
                candidate.Id,
                candidate.Name,
                candidate.Email,
                candidate.PhoneNumber,
                candidate.AppliedPosition,
                _dbContext.Set<InterviewProcess>()
                .Any(p => p.CandidateId == candidate.Id)
             )).ToListAsync();
    }
}
