using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace InterviewSystem.Application.InterviewTasks.Scheduling.RejectTask;

public sealed class RejectTaskHandler : IRequestHandler<RejectTaskCommand, Result<Guid>>
{
    private readonly IInterviewProcessRepository _interviewProcessRepository;
    private readonly IInterviewRoundRepository _interviewRoundRepository;
    private readonly IInterviewTaskRepository _interviewTaskRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUnitOfWorkRepository _uow;
    public RejectTaskHandler(IInterviewTaskRepository interviewTaskRepository, IUnitOfWorkRepository uow,
        IEmployeeRepository employeeRepository, IInterviewRoundRepository interviewRoundRepository,
        IInterviewProcessRepository interviewProcessRepository)
    {
        _interviewRoundRepository = interviewRoundRepository;
        _interviewTaskRepository = interviewTaskRepository;
        _employeeRepository = employeeRepository;
        _uow = uow;
        _interviewProcessRepository = interviewProcessRepository;
    }
    public async Task<Result<Guid>> Handle(RejectTaskCommand request, CancellationToken cancellationToken)
    {
        //task not valid
        var task = await _interviewTaskRepository.GetByIdAsync(request.TaskId);
        if (task is null)
            return Result<Guid>.Failure(GenericErrors.NoRecordFound(nameof(InterviewTask), request.TaskId));

        //assignee task ownership
        var isOwner = await _interviewTaskRepository
            .IsAssigneeOwnerAsync(taskId: request.TaskId, assigneeId: request.AssigneeId);
        if (isOwner is false)
            return Result<Guid>.Failure(InterviewTaskErrors.
                InvalidAction(assigneeId: request.AssigneeId, taskId: request.TaskId));

        //cannot reject if Task not active
        var activeTasks = await _interviewTaskRepository.GetAllByEmployeeIdAsync(request.AssigneeId);
        var isActiveTask = activeTasks.Any(t => t.Id == request.TaskId);
        if (isActiveTask is false)
            return Result<Guid>.Failure(InterviewTaskErrors.NoActiveTasks(request.AssigneeId));

        //cannot reject if theres not enough interviewee
        //AllowMultiple => interviewers > RoundItem.MaxInlineTasks
        //normal => interviewers > 1

        //get currentsequence, get AllowMultiple bool
        var currentRound = await _interviewRoundRepository.GetByIdAsync(task.InterviewRoundId);
        if (currentRound is null)
            return Result<Guid>.Failure(GenericErrors.NoRecordFound(nameof(InterviewRound), task.InterviewRoundId));

        var currentSequence = await _interviewRoundRepository
            .GetCurrentSequenceAsync(task.InterviewRoundId, task.RoundSequence);
        if (currentSequence is null)
            return Result<Guid>.Failure(GenericErrors.NoRecordFound(nameof(InterviewRoundItem), task.InterviewRoundId));

        List<Employee>? eligibleEmployees = new();

        var assigneeIds = (await _interviewTaskRepository.GetAllByProcessIdAndSequence
             (processId: task.InterviewProcessId, task.RoundSequence))
             .Select(it => it.AssigneeId)
             .ToHashSet();

        eligibleEmployees = (await _employeeRepository
            .GetEmployeesByPositionAsync(currentSequence.AllowedPosition, currentRound.Department))
            .Where(e => assigneeIds.Contains(e.Id) is false)
            .ToList();

        //no other eligible Assignee, lone
        if (eligibleEmployees.Any() is false &&
            task.RejectionOriginatorId is null)
            return Result<Guid>.Failure(InterviewTaskErrors.NoEligibleInterviewer());

        //TODO:full circle mechanism
        if (task.Rejected)
            return Result<Guid>.Failure(InterviewTaskErrors.CannotRejectAnymore());

        //set current Task to Rejected
        task.RejectTask(request.Reason);
        task.SetRejectionOriginator(task.AssigneeId);

        //update process to use the remaining Assignee for Interviewer (null for lone Assignee)
        var process = await _interviewProcessRepository.GetByIdAsync(task.InterviewProcessId);
        if (process is null)
            return Result<Guid>.Failure(GenericErrors.NoRecordFound
                (nameof(InterviewProcess), task.InterviewProcessId));

        var registeredAssignees = (await _interviewTaskRepository.GetAllByProcessIdAndSequence
                 (processId: task.InterviewProcessId, task.RoundSequence))
                 .Where(e => e.Id != request.AssigneeId)
                 .Select(it => new { it.AssigneeId, it.AssigneeName })
                 .FirstOrDefault();


        process.UpdateCurrentInterviewAfterReject
            (registeredAssignees?.AssigneeId, registeredAssignees?.AssigneeName);

        bool isFullCircle = (eligibleEmployees.Any() is false &&
                task.RejectionOriginatorId is not null);

        if (isFullCircle is false)
        {
            var eligibleEmployee = eligibleEmployees.FirstOrDefault();
            //create a new Task. Reject doesnt have to follow Create() rules
            var newTask = InterviewTask.Create(interviewRoundId: task.InterviewRoundId,
                interviewProcessId: task.InterviewProcessId,
                task.RoundSequence,
                task.CandidateId,
                task.CandidateName,
                eligibleEmployee!.Id,
                eligibleEmployee!.Name!);

            if (newTask.IsFailure)
                return Result<Guid>.Failure(newTask.Errors);

            if(task.RejectionOriginatorId is not null)
                newTask.Value.SetRejectionOriginator(task.RejectionOriginatorId.Value);
        }
        else
        {
            task.Reassignment();
        }

        await _uow.SaveChangesAsync();

        return Result<Guid>.Success(request.TaskId);
    }

}
