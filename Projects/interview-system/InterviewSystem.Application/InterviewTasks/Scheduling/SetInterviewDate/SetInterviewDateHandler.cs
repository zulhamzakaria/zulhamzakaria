using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace InterviewSystem.Application.InterviewTasks.Scheduling.SetInterviewDate;

public sealed class SetInterviewDateHandler : IRequestHandler<SetInterviewDateCommand, Result<DateTimeOffset>>
{
    private readonly IInterviewTaskRepository _interviewTaskRepository;
    private readonly IUnitOfWorkRepository _uow;
    public SetInterviewDateHandler(IUnitOfWorkRepository uow, IInterviewTaskRepository interviewTaskRepository)
    {
        _uow = uow;
        _interviewTaskRepository = interviewTaskRepository;
    }
    public async Task<Result<DateTimeOffset>> Handle(SetInterviewDateCommand request, CancellationToken cancellationToken)
    {
        var task = await _interviewTaskRepository.GetByIdAsync(request.TaskId);
        if (task is null)
            return Result<DateTimeOffset>
                .Failure(GenericErrors.NoRecordFound(nameof(InterviewTask), request.TaskId));

        var isOwner = await _interviewTaskRepository.IsAssigneeOwnerAsync(request.TaskId, request.AssigneeId);
        if (isOwner is false)
            return Result<DateTimeOffset>
                .Failure(InterviewTaskErrors.InvalidAction(request.AssigneeId,request.TaskId));

        var result = task.Scheduling(request.InterviewDate);
        if (result.IsFailure)
            return Result<DateTimeOffset>.Failure(result.Errors);

        await _uow.SaveChangesAsync();

        return Result<DateTimeOffset>.Success(request.InterviewDate);
    }
}
