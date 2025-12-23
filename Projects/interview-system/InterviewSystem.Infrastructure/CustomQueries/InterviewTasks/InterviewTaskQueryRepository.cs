using InterviewSystem.Infrastructure.CustomQueries.GetInterviewTasksSummary;
using InterviewSystem.Infrastructure.CustomQueries.InterviewTasks.DTOs;

namespace InterviewSystem.Infrastructure.CustomQueries.InterviewTasks;

public sealed class InterviewTaskQueryRepository : IInterviewTaskQueryRepository
{
    private readonly AppDbContext _dbContext;
    public InterviewTaskQueryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public Task<IReadOnlyCollection<InterviewTasksSummaryDTO>> GetSummaryAsync()
    {
        throw new NotImplementedException();
    }
}
