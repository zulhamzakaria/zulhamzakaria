using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;

namespace InterviewSystem.Application.InterviewRounds.GetInterviewRoundsSummary;

public sealed class Handler
{
    private readonly IInterviewRoundRepository _interviewRoundRepository;

    public Handler(IInterviewRoundRepository interviewRoundRepository)
    {
        _interviewRoundRepository = interviewRoundRepository;
    }

    public async Task<Result<IReadOnlyCollection<DTO>>> HandleAsync(Query query)
    {
        var results = (await _interviewRoundRepository.GetAllAsync())
            .Where(ir => query.EmployeeDepartment == null || ir.Department == query.EmployeeDepartment)
            .Where(ir => query.AppliedPosition == null || ir.AppliedPosition == query.AppliedPosition)
            .Select(MapToDTO)
            .ToList();

        if(results.Any() is false)
            return Result<IReadOnlyCollection<DTO>>.Failure(GenericErrors.NoRecordsFound(nameof(InterviewRound)));

        return Result<IReadOnlyCollection<DTO>>.Success(results);
    }

    private DTO MapToDTO(InterviewRound round) =>
        new DTO(
            round.Id,
            round.Department,
            round.AppliedPosition,
            round.NumberOfRounds
            );


}
