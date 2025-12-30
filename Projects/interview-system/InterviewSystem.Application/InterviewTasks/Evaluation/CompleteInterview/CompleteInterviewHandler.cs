using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace InterviewSystem.Application.InterviewTasks.Evaluation.CompleteInterview;

public sealed class CompleteInterviewHandler : IRequestHandler<CompleteInterviewCommand, Result<Guid>>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IInterviewTaskRepository _interviewTaskRepository;
    private readonly IInterviewRoundRepository _interviewRoundRepository;
    private readonly IInterviewProcessRepository _interviewProcessRepository;
    private readonly IUnitOfWorkRepository _uow;

    public CompleteInterviewHandler(IInterviewTaskRepository interviewTaskRepository,
        IUnitOfWorkRepository uow, IInterviewProcessRepository interviewProcessRepository,
        IInterviewRoundRepository interviewRoundRepository, IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
        _interviewTaskRepository = interviewTaskRepository;
        _interviewRoundRepository = interviewRoundRepository;
        _interviewProcessRepository = interviewProcessRepository;
        _uow = uow;
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

        //get next sequence
        var nextRoundItem = await _interviewRoundRepository.GetNextSequence
            (roundId: task.InterviewRoundId,
            currentSequence: task.RoundSequence);
        var currrentRoundItem = await _interviewRoundRepository.GetCurrentSequence
            (roundId: task.InterviewRoundId,
            currentSequence: task.RoundSequence);

        if (nextRoundItem is null && currrentRoundItem?.CanCompleteProcess is false)
            return Result<Guid>.Failure(InterviewRoundErrors.InvalidPolicy());

        //get next interviewer

        //update process
        var process = await _interviewProcessRepository.GetByIdAsync(task.InterviewProcessId);
        if (process is null)
            return Result<Guid>.Failure(GenericErrors.NoRecordFound(nameof(InterviewProcess),
                task.InterviewProcessId));

        if (request.Passed is false)
        {
            //update InterviewProcessStatus to Failed
            process.MarkFailed();
        }
        else if (nextRoundItem is null 
            && currrentRoundItem?.CanCompleteProcess is true 
            && request.Passed is true)
        {
            process.MarkCompleted();
        }
        else
        {



            //update interviewer, roundsequence
            process.Advance(nextSequence: nextRoundItem.Sequence,
                nextInterviewerId: new Guid(),
                nextInterviewerName: string.Empty);
        }


        //create new task if needed

        await _uow.SaveChangesAsync();

        return Result<Guid>.Success(request.TaskId);
    }
}
