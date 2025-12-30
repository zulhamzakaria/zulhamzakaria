using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace InterviewSystem.Application.InterviewTasks.Evaluation.CompleteInterview;

public sealed class PassInterviewHandler : IRequestHandler<PassInterviewCommand, Result<Guid>>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IInterviewTaskRepository _interviewTaskRepository;
    private readonly IInterviewRoundRepository _interviewRoundRepository;
    private readonly IInterviewProcessRepository _interviewProcessRepository;
    private readonly IUnitOfWorkRepository _uow;

    public PassInterviewHandler(IInterviewTaskRepository interviewTaskRepository,
        IUnitOfWorkRepository uow, IInterviewProcessRepository interviewProcessRepository,
        IInterviewRoundRepository interviewRoundRepository, IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
        _interviewTaskRepository = interviewTaskRepository;
        _interviewRoundRepository = interviewRoundRepository;
        _interviewProcessRepository = interviewProcessRepository;
        _uow = uow;
    }

    public async Task<Result<Guid>> Handle(PassInterviewCommand request, CancellationToken cancellationToken)
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
        var nextRoundItem = await _interviewRoundRepository.GetNextSequenceAsync
            (roundId: task.InterviewRoundId,
            currentSequence: task.RoundSequence);
        var currrentRoundItem = await _interviewRoundRepository.GetCurrentSequenceAsync
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

            await _uow.SaveChangesAsync();

            return Result<Guid>.Success(request.TaskId);
        }

        //get next interviewer
        var eligibleEmployees = await _employeeRepository
            .GetEmployeesByPositionAsync(nextRoundItem.AllowedPosition,
            process.Department);

        var nextInterviewer = eligibleEmployees.FirstOrDefault();

        if (nextInterviewer is null)
            return Result<Guid>.Failure(InterviewTaskErrors.NoEligibleInterviewer());

        //update interviewer, roundsequence
        var updatedProcess = process.Advance(nextSequence: nextRoundItem.Sequence,
            nextInterviewer.Id,
            nextInterviewer.Name);
        if (updatedProcess.IsFailure)
            return Result<Guid>.Failure(updatedProcess.Errors);

        //update current task
        var updatedTask = task.MarkCompleted(true, request.Reason);
        if (updatedTask.IsFailure)
            return Result<Guid>.Failure(updatedTask.Errors);

        //create next task
        var newTask = InterviewTask.Create(
            interviewRoundId: task.InterviewRoundId,
            interviewProcessId: process.Id,
            roundSequence: nextRoundItem.Sequence,
            candidateId: task.CandidateId,
            candidateName: task.CandidateName,
            assigneeId: nextInterviewer.Id,
            assigneeName: nextInterviewer.Name ?? string.Empty);

        if (newTask.IsFailure)
            return Result<Guid>.Failure(newTask.Errors);

        //await _interviewTaskRepository.AddAsync(newTask.Value!);

        await _uow.SaveChangesAsync();

        return Result<Guid>.Success(newTask.Value!.Id);
    }

}
