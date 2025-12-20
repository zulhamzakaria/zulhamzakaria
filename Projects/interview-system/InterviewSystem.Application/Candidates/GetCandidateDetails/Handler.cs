using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;

namespace InterviewSystem.Application.Candidates.GetCandidateDetails;

public sealed class Handler
{
    private readonly ICandidateRepository _candidateRepository;

    public Handler(ICandidateRepository candidateRepository)
    {
        _candidateRepository = candidateRepository;
    }

    public async Task<Result<DTO>> HandlerAsync(Query query)
    {
        var result = await _candidateRepository.GetByIdAsync(query.Id);

        if (result is null)
            return Result<DTO>.Failure(GenericErrors.NoRecordFound(nameof(Candidate), query.Id));

        var returnResult = MapToDto(result);

        return Result<DTO>.Success(returnResult);
    }

    private DTO MapToDto(Candidate candidate) =>
        new DTO(candidate.Id,
            candidate.Name,
            candidate.Email,
            candidate.PhoneNumber,
            candidate.AppliedPosition);

}
