using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace InterviewSystem.Application.Candidates.GetCandidatesSummary;

public sealed class GetCandidatesSummaryHandler : IRequestHandler<GetCandidatesSummaryQuery, Result<IReadOnlyCollection<GetCandidatesSummaryDTO>>>
{
    private readonly ICandidatesSummaryQueryRepository _candidatesSummaryRepository;
    private readonly IInterviewProcessRepository _interviewProcessRepository;

    public GetCandidatesSummaryHandler(IInterviewProcessRepository interviewProcessRepository, 
        ICandidatesSummaryQueryRepository candidatesSummaryRepository)
    {
        _interviewProcessRepository = interviewProcessRepository;
        _candidatesSummaryRepository = candidatesSummaryRepository;
    }

    public async Task<Result<IReadOnlyCollection<GetCandidatesSummaryDTO>>> Handle(GetCandidatesSummaryQuery request, CancellationToken cancellationToken)
    {
        var candidates = await _candidatesSummaryRepository.GetSummaryAsync();

        var filteredCandidates = candidates
            .Where(c => request.AppliedPosition == null || c.AppliedPosition == request.AppliedPosition)
            .Where(c => !request.SubmittedForInterview.HasValue)
            .ToList();

        if (filteredCandidates.Any() is false)
            return Result<IReadOnlyCollection<GetCandidatesSummaryDTO>>.Failure(GenericErrors.NoRecordsFound(nameof(Candidate)));

        return Result<IReadOnlyCollection<GetCandidatesSummaryDTO>>.Success(filteredCandidates);
    }

}
