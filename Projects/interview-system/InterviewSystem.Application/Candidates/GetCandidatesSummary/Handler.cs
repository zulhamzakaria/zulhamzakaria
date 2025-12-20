using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;

namespace InterviewSystem.Application.Candidates.GetCandidatesSummary;

public sealed class Handler
{
    private readonly ICandidateRepository _candidateRepository;

    public Handler(ICandidateRepository candidateRepository)
    {
        _candidateRepository = candidateRepository;
    }

    public async Task<Result<IReadOnlyCollection<DTO>>> HandleAsync(Query query)
    {
        var candidates = await _candidateRepository.GetAllAsync();

        var filteredCandidates = candidates
            .Where(c => query.AppliedPosition == null || c.AppliedPosition == query.AppliedPosition)
            .Select(MapToDTO)
            .ToList();

        if (filteredCandidates.Any() is false)
            return Result<IReadOnlyCollection<DTO>>.Failure(GenericErrors.NoRecordsFound(nameof(Candidate)));

        return Result<IReadOnlyCollection<DTO>>.Success(filteredCandidates);
    }

    private DTO MapToDTO(Candidate candidate) =>
        new DTO(candidate.Id,
            candidate.Name,
            candidate.Email,
            candidate.PhoneNumber,
            candidate.AppliedPosition);
}
