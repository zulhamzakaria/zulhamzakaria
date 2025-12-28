using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace InterviewSystem.Application.InterviewTasks.CreateInterviewTask;

public sealed class CreateInterviewTaskHandler : IRequestHandler<CreateInterviewTaskCommand, Result<Guid>>
{
    private readonly IUnitOfWorkRepository _uow;
    private readonly IInterviewTaskRepository _interviewTaskRepository;

    public CreateInterviewTaskHandler(IInterviewTaskRepository interviewTaskRepository, IUnitOfWorkRepository uow)
    {
        _interviewTaskRepository = interviewTaskRepository;
        _uow = uow;
    }

    public async Task<Result<Guid>> Handle(CreateInterviewTaskCommand request, CancellationToken cancellationToken)
    {
        var result = InterviewTask.Create(
           request.InterviewRoundId,
           request.InterviewProcessId,
           roundSequence: 1,
           request.CandidateId,
           request.CandidateName,
           request.AssigneeId,
           request.AssigneeName);

        if (result.IsFailure)
            return Result<Guid>.Failure(result.Errors);

        var newTask = result.Value!;

        await _interviewTaskRepository.AddAsync(newTask);
        await _uow.SaveChangesAsync();

        return Result<Guid>.Success(newTask.Id);
    }

}
