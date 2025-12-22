using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;

namespace InterviewSystem.Application.InterviewRounds.CreateInterviewRound;

public sealed class Handler
{
    private readonly IInterviewRoundRepository _interviewRoundRepository;
    private readonly IUnitOfWorkRepository _uow;
    public Handler(IInterviewRoundRepository interviewRoundRepository, IUnitOfWorkRepository uow)
    {
        _interviewRoundRepository = interviewRoundRepository;
        _uow = uow;
    }

    public async Task<Result<Guid>> HandleAsync(Command command)
    {
        var result = InterviewRound.Create(
            command.EmployeeDepartment,
            command.AppliedPosition,
            command.EmployeePositions);

        if (result.IsFailure)
            return Result<Guid>.Failure(result.Errors);

        var newInterviewRound = result.Value!;

        await _interviewRoundRepository.AddAsync(newInterviewRound);
        await _uow.SaveChangesAsync();

        return Result<Guid>.Success(newInterviewRound.Id);
    }
}
