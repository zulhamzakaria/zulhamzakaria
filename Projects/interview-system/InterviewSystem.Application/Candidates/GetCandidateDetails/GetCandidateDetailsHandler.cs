using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace InterviewSystem.Application.Candidates.GetCandidateDetails;

public sealed class GetCandidateDetailsHandler : IRequestHandler<GetCandidateDetailsQuery, Result<GetCandidateDetailsDTO>>
{
    private readonly ICandidateRepository _candidateRepository;

    public GetCandidateDetailsHandler(ICandidateRepository candidateRepository)
    {
        _candidateRepository = candidateRepository;
    }

    public async Task<Result<GetCandidateDetailsDTO>> Handle(GetCandidateDetailsQuery request, CancellationToken cancellationToken)
    {
        var result = await _candidateRepository.GetByIdAsync(request.Id);

        if (result is null)
            return Result<GetCandidateDetailsDTO>.Failure(GenericErrors.NoRecordFound(nameof(Candidate), request.Id));

        var returnResult = MapToDto(result);

        return Result<GetCandidateDetailsDTO>.Success(returnResult);
    }

    private GetCandidateDetailsDTO MapToDto(Candidate candidate) =>
        new GetCandidateDetailsDTO(candidate.Id,
            candidate.Name,
            candidate.Email,
            candidate.PhoneNumber,
            candidate.AppliedPosition);

}
