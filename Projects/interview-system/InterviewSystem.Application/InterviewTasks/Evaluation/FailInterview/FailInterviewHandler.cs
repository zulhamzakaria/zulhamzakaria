using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace InterviewSystem.Application.InterviewTasks.Evaluation.FailInterview;

public sealed class FailInterviewHandler : IRequestHandler<FailInterviewCommand, Result<Guid>>
{
    private readonly IUnitOfWorkRepository _uow;
    private readonly IInterviewProcessRepository _interviewProcessRepository;
    private readonly IInterviewTaskRepository _interviewTaskRepository;

    public FailInterviewHandler(IUnitOfWorkRepository uow, IInterviewTaskRepository interviewTaskRepository,
        IInterviewProcessRepository interviewProcessRepository)
    {
        _uow = uow;
        _interviewTaskRepository = interviewTaskRepository;
        _interviewProcessRepository = interviewProcessRepository;
    }

    public async Task<Result<Guid>> Handle(FailInterviewCommand request, CancellationToken cancellationToken)
    {
        //task existance
        var task = await _interviewTaskRepository.GetByIdAsync(request.TaskId);
        if (task is null)
            return Result<Guid>.Failure(GenericErrors.NoRecordFound(nameof(InterviewTask), request.TaskId));

        //task ownership
        var isOwner = await _interviewTaskRepository.IsAssigneeOwnerAsync(taskId: request.TaskId,
            assigneeId: request.AssigneeId);
        if (isOwner is false)
            return Result<Guid>.Failure(InterviewTaskErrors.InvalidAction(
                assigneeId: request.AssigneeId,
                taskId: request.TaskId));

        var process = await _interviewProcessRepository.GetByIdAsync(task.InterviewProcessId);
        if(process is null)
            return Result<Guid>.Failure(GenericErrors.NoRecordFound(nameof(InterviewProcess), task.InterviewProcessId));

        process.MarkFailed();
        task.MarkCompleted(false, request.Reason);

        await _uow.SaveChangesAsync();

        return Result<Guid>.Success(request.TaskId);
    }
}
