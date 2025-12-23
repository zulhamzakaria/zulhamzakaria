using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;

namespace InterviewSystem.Application.InterviewRounds.GetInterviewRoundDetails;

public sealed class GetInterviewRoundDetailsHandler
{
    private readonly IInterviewRoundRepository _interviewRoundRepository;

    public GetInterviewRoundDetailsHandler(IInterviewRoundRepository interviewRoundRepository)
    {
        _interviewRoundRepository = interviewRoundRepository;
    }

    public async Task<Result<GetInterviewRoundDetailsDTO>> HandleAsync(GetInterviewRoundDetailsQuery query)
    {
        var result = await _interviewRoundRepository.GetByIdAsync(query.Id);

        if (result is null)
            return Result<GetInterviewRoundDetailsDTO>.Failure(GenericErrors.NoRecordFound(nameof(InterviewRound), query.Id));

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
