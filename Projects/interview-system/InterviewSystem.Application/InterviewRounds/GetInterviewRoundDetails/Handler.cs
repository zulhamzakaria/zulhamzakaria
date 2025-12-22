using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;

namespace InterviewSystem.Application.InterviewRounds.GetInterviewRoundDetails;

public sealed class Handler
{
    private readonly IInterviewRoundRepository _interviewRoundRepository;

    public Handler(IInterviewRoundRepository interviewRoundRepository)
    {
        _interviewRoundRepository = interviewRoundRepository;
    }

    public async Task<Result<DTO>> HandleAsync(Query query)
    {
        var result = await _interviewRoundRepository.GetByIdAsync(query.Id);

        if (result is null)
            return Result<DTO>.Failure(GenericErrors.NoRecordFound(nameof(InterviewRound), query.Id));

        var returnResult = MapToDTO(result);

        return Result<DTO>.Success(returnResult);
    }

    private DTO MapToDTO(InterviewRound round) =>
        new DTO(
            round.Id,
            round.Department,
            round.NumberOfRounds,
            round.AppliedPosition,
            round.Items
            );
}
