using InterviewSystem.Infrastructure.CustomQueries.InterviewTasks.DTOs;

namespace InterviewSystem.Infrastructure.CustomQueries.GetInterviewTasksSummary;

public interface IInterviewTaskQueryRepository
{
    Task<IReadOnlyCollection<InterviewTasksSummaryDTO>> GetSummaryAsync();
}
