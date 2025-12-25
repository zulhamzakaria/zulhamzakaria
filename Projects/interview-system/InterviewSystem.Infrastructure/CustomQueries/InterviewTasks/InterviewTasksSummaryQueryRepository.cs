using InterviewSystem.Application.InterviewTasks.GetInterviewTasksSummary;
using InterviewSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace InterviewSystem.Infrastructure.CustomQueries.InterviewTasks;

public sealed class InterviewTasksSummaryQueryRepository : IInterviewTasksSummaryQueryRepository
{
    private readonly AppDbContext _dbContext;
    public InterviewTasksSummaryQueryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private IQueryable<InterviewTask> Query() =>
        _dbContext.Set<InterviewTask>().AsNoTracking();

    public async Task<IReadOnlyCollection<GetInterviewTasksSummaryDTO>> GetSummaryAsync()
    {
        return await (
            from task in Query()
            join round in _dbContext.Set<InterviewRound>()
            on task.InterviewRoundId equals round.Id
            select new GetInterviewTasksSummaryDTO(
                task.Id,
                task.CandidateName,
                round.Department,
                round.AppliedPosition,
                task.InterviewTaskStatus,
                task.AssignedAt,
                task.Rejected,
                task.TaskRejectionReason
            )).ToListAsync();
    }
}
