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

        var isOwner = await _interviewTaskRepository.IsAssigneeOwnerAsync(taskId: request.TaskId,
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


        //passed interview, no more next round
        if (nextRoundItem is null)
        {
            process.MarkCompleted();
            task.MarkCompleted(true, request.Reason);

            await _uow.SaveChangesAsync();

            return Result<Guid>.Success(request.TaskId);
        }

        //advance to the next round
        var result = await CanAdvance(nextRoundItem, process, task);

        if (result.IsFailure)
            return Result<Guid>.Failure(result.Errors);

        return Result<Guid>.Success(result.Value);
    }

    private async Task<Result<Guid>> CanAdvance(InterviewRoundItem nextRoundItem, InterviewProcess process,
        InterviewTask currentTask)
    {
        //get next interviewer
        var eligibleEmployees = await _employeeRepository
            .GetEmployeesByPositionAsync(nextRoundItem.AllowedPosition);

        var nextInterviewer = eligibleEmployees.FirstOrDefault();

        if (nextInterviewer is null)
            return Result<Guid>.Failure(InterviewTaskErrors.NoEligibleInterviewer());

        //update interviewer, roundsequence
        process.Advance(nextSequence: nextRoundItem.Sequence,
            nextInterviewer.Id ,
            nextInterviewer.Name);

        //create next task
        var task = InterviewTask.Create(
            interviewRoundId: currentTask.InterviewRoundId,
            interviewProcessId: process.Id,
            roundSequence: nextRoundItem.Sequence,
            candidateId: currentTask.CandidateId,
            candidateName: currentTask.CandidateName,
            assigneeId: nextInterviewer.Id,
            assigneeName: nextInterviewer.Name ?? string.Empty);

        if (task.IsFailure)
            return Result<Guid>.Failure(task.Errors);

        await _uow.SaveChangesAsync();

        return Result<Guid>.Success(task.Value.Id);
    }
}
