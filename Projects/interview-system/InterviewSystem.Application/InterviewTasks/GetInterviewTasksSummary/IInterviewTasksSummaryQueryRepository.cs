namespace InterviewSystem.Application.InterviewTasks.GetInterviewTasksSummary;

public interface IInterviewTasksSummaryQueryRepository
{
    Task<IReadOnlyCollection<GetInterviewTasksSummaryDTO>> GetSummaryAsync();
}
