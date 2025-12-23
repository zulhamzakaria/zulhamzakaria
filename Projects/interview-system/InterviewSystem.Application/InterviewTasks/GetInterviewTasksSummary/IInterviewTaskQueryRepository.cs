namespace InterviewSystem.Application.InterviewTasks.GetInterviewTasksSummary;

public interface IInterviewTaskQueryRepository
{
    Task<IReadOnlyCollection<InterviewTasksSummaryDTO>> GetSummaryAsync();
}
