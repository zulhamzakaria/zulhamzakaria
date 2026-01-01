namespace InterviewSystem.Application.Candidates.GetCandidatesSummary;

public interface ICandidatesSummaryQueryRepository
{
    Task<IReadOnlyCollection<GetCandidatesSummaryDTO>> GetSummaryAsync();
}
