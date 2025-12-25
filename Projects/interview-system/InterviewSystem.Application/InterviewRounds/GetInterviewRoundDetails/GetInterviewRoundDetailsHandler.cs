using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace InterviewSystem.Application.InterviewRounds.GetInterviewRoundDetails;

public sealed class GetInterviewRoundDetailsHandler : 
    IRequestHandler<GetInterviewRoundDetailsQuery, Result<GetInterviewRoundDetailsDTO>>
{
    private readonly IInterviewRoundRepository _interviewRoundRepository;

    public GetInterviewRoundDetailsHandler(IInterviewRoundRepository interviewRoundRepository)
    {
        _interviewRoundRepository = interviewRoundRepository;
    }

    public async Task<Result<GetInterviewRoundDetailsDTO>> Handle(GetInterviewRoundDetailsQuery request, CancellationToken cancellationToken)
    {
        var result = await _interviewRoundRepository.GetByIdAsync(request.Id);

        if (result is null)
            return Result<GetInterviewRoundDetailsDTO>.Failure(GenericErrors.NoRecordFound(nameof(InterviewRound), request.Id));

        var returnResult = MapToDTO(result);

        return Result<GetInterviewRoundDetailsDTO>.Success(returnResult);
    }

    private GetInterviewRoundDetailsDTO MapToDTO(InterviewRound round) =>
        new GetInterviewRoundDetailsDTO(
            round.Id,
            round.Department,
            round.NumberOfRounds,
            round.AppliedPosition,
            round.Items
            );
}
