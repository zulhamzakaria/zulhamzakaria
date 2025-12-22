using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;

namespace InterviewSystem.Application.InterviewTasks.CreateInterviewTask;

public sealed class Handler
{
    private readonly IUnitOfWorkRepository _uow;
    private readonly IInterviewTaskRepository _interviewTaskRepository;

    public Handler(IInterviewTaskRepository interviewTaskRepository, IUnitOfWorkRepository uow)
    {
        _interviewTaskRepository = interviewTaskRepository;
        _uow = uow;
    }

    public async Task<Result<Guid>> HandleAsync(Command command)
    {
        var result =  InterviewTask.Create(
            command.InterviewRoundId, 
            command.CandidateId);

        if (result.IsFailure)
            return Result<Guid>.Failure(result.Errors);

        var newTask = result.Value!;

        await _interviewTaskRepository.AddAsync(newTask);
        await _uow.SaveChangesAsync();

        return Result<Guid>.Success(newTask.Id);

    }
}
