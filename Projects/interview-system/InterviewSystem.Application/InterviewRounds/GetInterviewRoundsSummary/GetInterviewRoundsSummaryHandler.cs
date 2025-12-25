using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace InterviewSystem.Application.InterviewRounds.GetInterviewRoundsSummary;

public sealed class GetInterviewRoundsSummaryHandler :
    IRequestHandler<GetInterviewRoundsSummaryQuery, Result<IReadOnlyCollection<GetInterviewRoundsSummaryDTO>>>
{
    private readonly IInterviewRoundRepository _interviewRoundRepository;

    public GetInterviewRoundsSummaryHandler(IInterviewRoundRepository interviewRoundRepository)
    {
        _interviewRoundRepository = interviewRoundRepository;
    }

    public async Task<Result<IReadOnlyCollection<GetInterviewRoundsSummaryDTO>>> Handle
        (GetInterviewRoundsSummaryQuery request, CancellationToken cancellationToken)
    {
        var results = (await _interviewRoundRepository.GetAllAsync())
           .Where(ir => request.EmployeeDepartment == null || ir.Department == request.EmployeeDepartment)
           .Where(ir => request.AppliedPosition == null || ir.AppliedPosition == request.AppliedPosition)
           .Select(MapToDTO)
           .ToList();

        if (results.Any() is false)
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
