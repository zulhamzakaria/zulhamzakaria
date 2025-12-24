using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;

namespace InterviewSystem.Application.InterviewTasks.CreateInterviewTask;

public sealed class CreateInterviewTaskHandler
{
    private readonly IUnitOfWorkRepository _uow;
    private readonly IInterviewTaskRepository _interviewTaskRepository;

    public CreateInterviewTaskHandler(IInterviewTaskRepository interviewTaskRepository, IUnitOfWorkRepository uow)
    {
        _interviewTaskRepository = interviewTaskRepository;
        _uow = uow;
    }

    public async Task<Result<Guid>> HandleAsync(CreateInterviewTaskCommand command)
    {
        var result =  InterviewTask.Create(
            command.InterviewRoundId, 
            command.InterviewProcessId,
            command.CandidateId,
            command.CandidateName,
            command.AssigneeId,
            command.AssigneeName);

        if (result.IsFailure)
            return Result<Guid>.Failure(result.Errors);

        var newTask = result.Value!;

        await _interviewTaskRepository.AddAsync(newTask);
        await _uow.SaveChangesAsync();

        return Result<Guid>.Success(newTask.Id);

    }
}
