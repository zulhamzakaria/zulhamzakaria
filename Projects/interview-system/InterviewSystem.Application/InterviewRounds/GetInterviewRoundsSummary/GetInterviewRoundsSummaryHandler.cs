using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;

namespace InterviewSystem.Application.InterviewRounds.GetInterviewRoundsSummary;

public sealed class GetInterviewRoundsSummaryHandler
{
    private readonly IInterviewRoundRepository _interviewRoundRepository;

    public GetInterviewRoundsSummaryHandler(IInterviewRoundRepository interviewRoundRepository)
    {
        _interviewRoundRepository = interviewRoundRepository;
    }

    public async Task<Result<IReadOnlyCollection<GetInterviewRoundsSummaryDTO>>> HandleAsync(GetInterviewRoundsSummaryQuery query)
    {
        var results = (await _interviewRoundRepository.GetAllAsync())
            .Where(ir => query.EmployeeDepartment == null || ir.Department == query.EmployeeDepartment)
            .Where(ir => query.AppliedPosition == null || ir.AppliedPosition == query.AppliedPosition)
            .Select(MapToDTO)
            .ToList();

        if(results.Any() is false)
            return Result<IReadOnlyCollection<GetInterviewRoundsSummaryDTO>>.Failure(GenericErrors.NoRecordsFound(nameof(InterviewRound)));

        return Result<IReadOnlyCollection<GetInterviewRoundsSummaryDTO>>.Success(results);
    }

    private GetInterviewRoundsSummaryDTO MapToDTO(InterviewRound round) =>
        new GetInterviewRoundsSummaryDTO(
            round.Id,
            round.Department,
            round.AppliedPosition,
            round.NumberOfRounds
            );


}
