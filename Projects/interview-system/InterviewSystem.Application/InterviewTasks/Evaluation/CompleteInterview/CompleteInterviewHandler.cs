using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace InterviewSystem.Application.InterviewTasks.Evaluation.CompleteInterview;

public sealed class CompleteInterviewHandler : IRequestHandler<CompleteInterviewCommand, Result<Guid>>
{
    private readonly IInterviewTaskRepository _interviewTaskRepository;
    private readonly IInterviewProcessRepository _interviewProcessRepository;
    private readonly IUnitOfWorkRepository _uow;

    public CompleteInterviewHandler(IInterviewTaskRepository interviewTaskRepository,
        IUnitOfWorkRepository uow, IInterviewProcessRepository interviewProcessRepository)
    {
        _interviewTaskRepository = interviewTaskRepository;
        _uow = uow;
        _interviewProcessRepository = interviewProcessRepository;
    }

    public async Task<Result<Guid>> Handle(CompleteInterviewCommand request, CancellationToken cancellationToken)
    {
        var task = await _interviewTaskRepository.GetByIdAsync(request.TaskId);
        if (task is null)
            return Result<Guid>.Failure(GenericErrors.NoRecordFound(nameof(InterviewTask), request.TaskId));

        var isOwner = await _interviewTaskRepository.IsAssigneeOwner(taskId: request.TaskId,
           assigneeId: request.AssigneeId);
        if (isOwner is false)
            return Result<Guid>.Failure(InterviewTaskErrors.InvalidAction(
                assigneeId: request.AssigneeId,
                taskId: request.TaskId));

        //update process
        var process = await _interviewProcessRepository.GetByIdAsync(task.InterviewProcessId);
        if (process is null)
            return Result<Guid>.Failure(GenericErrors.NoRecordFound(nameof(InterviewProcess),
                task.InterviewProcessId));

        if (request.Passed is false)
        {
            //update InterviewProcessStatus
        }
        else
        {
            //update interviewer
            //update roundsequence
        }


        //create new task if needed

        await _uow.SaveChangesAsync();

        return Result<Guid>.Success(request.TaskId);
    }
}
