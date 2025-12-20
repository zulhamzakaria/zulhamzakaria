using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;

namespace InterviewSystem.Application.InterviewProcesses.CreateInterviewProcess;

public sealed class Handler
{
    private readonly IUnitOfWorkRepository _uow;
    private readonly IInterviewProcessRepository _interviewProcessRepository;

    public Handler(IInterviewProcessRepository interviewProcessRepository, IUnitOfWorkRepository uow)
    {
        _interviewProcessRepository = interviewProcessRepository;
        _uow = uow;
    }

    public async Task<Result<Guid>> HandleAsync(Command command)
    {
        var result = InterviewProcess.Create(command.CandidateId,
            command.CandidateName,
            command.EmployeeDepartment,
            command.CurrentSequence,
            command.InterviewProcessStatus,
            command.CurrentInterviewId,
            command.CurrentInterviewName);

        if (result.IsFailure)
            return Result<Guid>.Failure(result.Errors);

        var newProcess = result.Value;

        await _interviewProcessRepository.AddAsync(newProcess!);
        await _uow.SaveChangesAsync();

        return Result<Guid>.Success(newProcess!.Id);

    }

}
