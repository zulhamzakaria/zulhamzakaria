namespace InterviewSystem.Application.InterviewTasks.GetInterviewTasksSummary;

public interface IInterviewTaskQueryRepository
{
    Task<IReadOnlyCollection<GetInterviewTasksSummaryDTO>> GetSummaryAsync();
}
