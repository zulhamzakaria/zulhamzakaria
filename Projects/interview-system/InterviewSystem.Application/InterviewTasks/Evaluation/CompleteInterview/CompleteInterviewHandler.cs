using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;
using MediatR;
using System;
using System.Diagnostics;

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

        //update process
        var process = await _interviewProcessRepository.GetByIdAsync(task.InterviewProcessId);
        if (process is null)
            return Result<Guid>.Failure(GenericErrors.NoRecordFound(nameof(InterviewProcess),
                task.InterviewProcessId));


        if(nextRoundItem is not null) 
        {
            CanAdvance(nextRoundItem: nextRoundItem, process: process);
        }
        else
        {
            ProcessCompleted(passed: request.Passed, 
                canCompleteProcess: currrentRoundItem.CanCompleteProcess, 
                process: process);
        }

        await _uow.SaveChangesAsync();

        return Result<Guid>.Success(request.TaskId);
    }

    private Result<Unit> CanAdvance(InterviewRoundItem nextRoundItem, InterviewProcess process)
    {
        //get next interviewer
        var nextInterviewer = _employeeRepository.GetEmployeesByPositionAsync(nextRoundItem.AllowedPosition);
        if(nextInterviewer is null)
            return Result<Unit>.Failure()

        //update interviewer, roundsequence
        process.Advance(nextSequence: nextRoundItem.Sequence,
            nextInterviewerId: new Guid(),
            nextInterviewerName: string.Empty);

        return Result<Unit>.Success(new Unit());
    }

    private void ProcessCompleted(bool passed, bool canCompleteProcess, InterviewProcess process)
    {
        if (passed is false)
        {
            process.MarkFailed();
        }
        else if (canCompleteProcess is true && passed is true)
        {
            process.MarkCompleted();
        }
    }



}
