using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace InterviewSystem.Application.Candidates.GetCandidatesSummary;

public sealed class GetCandidatesSummaryHandler : IRequestHandler<GetCandidatesSummaryQuery, Result<IReadOnlyCollection<GetCandidatesSummaryDTO>>>
{
    private readonly ICandidateRepository _candidateRepository;

    public GetCandidatesSummaryHandler(ICandidateRepository candidateRepository)
    {
        _candidateRepository = candidateRepository;
    }

    public async Task<Result<IReadOnlyCollection<GetCandidatesSummaryDTO>>> Handle(GetCandidatesSummaryQuery request, CancellationToken cancellationToken)
    {
        var candidates = await _candidateRepository.GetAllAsync();

        var filteredCandidates = candidates
            .Where(c => request.AppliedPosition == null || c.AppliedPosition == request.AppliedPosition)
            .Select(MapToDTO)
            .ToList();

        if (filteredCandidates.Any() is false)
            return Result<IReadOnlyCollection<GetCandidatesSummaryDTO>>.Failure(GenericErrors.NoRecordsFound(nameof(Candidate)));

        return Result<IReadOnlyCollection<GetCandidatesSummaryDTO>>.Success(filteredCandidates);
    }

    private GetCandidatesSummaryDTO MapToDTO(Candidate candidate) =>
        new GetCandidatesSummaryDTO(candidate.Id,
            candidate.Name,
            candidate.Email,
            candidate.PhoneNumber,
            candidate.AppliedPosition);
}
