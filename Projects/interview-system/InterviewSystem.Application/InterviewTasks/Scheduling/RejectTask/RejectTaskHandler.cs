using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace InterviewSystem.Application.InterviewTasks.Scheduling.RejectTask;

public sealed class RejectTaskHandler : IRequestHandler<RejectTaskCommand, Result<Guid>>
{
    private readonly IInterviewRoundRepository _interviewRoundRepository;
    private readonly IInterviewTaskRepository _interviewTaskRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUnitOfWorkRepository _uow;
    public RejectTaskHandler(IInterviewTaskRepository interviewTaskRepository, IUnitOfWorkRepository uow,
        IEmployeeRepository employeeRepository, IInterviewRoundRepository interviewRoundRepository)
    {
        _interviewRoundRepository = interviewRoundRepository;
        _interviewTaskRepository = interviewTaskRepository;
        _employeeRepository = employeeRepository;
        _uow = uow;
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

        if (currentSequence.AllowMultiple)
        {
            var assigneeIds = (await _interviewTaskRepository.GetAllByProcessIdAndSequence
                 (processId: task.InterviewProcessId, task.RoundSequence))
                 .Select(it => it.AssigneeId)
                 .ToHashSet();

            eligibleEmployees = (await _employeeRepository
                .GetEmployeesByPositionAsync(currentSequence.AllowedPosition, currentRound.Department))
                .Where(e => assigneeIds.Contains(e.Id) is false)
                .ToList();
        }
        else
        {
            eligibleEmployees = (await _employeeRepository
                  .GetEmployeesByPositionAsync(currentSequence.AllowedPosition, currentRound.Department))
                  .Where(e => e.Id != request.AssigneeId)
                  .ToList();
        }

        //no other eligible Assignee, lone
        if (eligibleEmployees.Any() is false)
            return Result<Guid>.Failure(InterviewTaskErrors.NoEligibleInterviewer());

        //set current Task to Rejected
       

        return Result<Guid>.Success(request.TaskId);
    }
}
