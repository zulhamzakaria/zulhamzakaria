using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace InterviewSystem.Application.InterviewTasks.Scheduling.Rescheduling;

public sealed class ReschedulingHandler : IRequestHandler<ReschedulingCommand, Result<DateTimeOffset>>
{
    private readonly IInterviewTaskRepository _interviewTaskRepository;
    private readonly IUnitOfWorkRepository _uow;

    public ReschedulingHandler(IInterviewTaskRepository interviewTaskRepository, IUnitOfWorkRepository uow)
    {
        _interviewTaskRepository = interviewTaskRepository;
        _uow = uow;
    }

    public async Task<Result<DateTimeOffset>> Handle(ReschedulingCommand request, CancellationToken cancellationToken)
    {
        var isOwner = await _interviewTaskRepository.IsAssigneeOwner(request.TaskId, request.AssigneeId);
        if (isOwner is false)
            return Result<DateTimeOffset>
                .Failure(InterviewTaskErrors.InvalidAction(request.AssigneeId, request.TaskId));

        var task = await _interviewTaskRepository.GetByIdAsync(request.TaskId);
        if (task is null)
            return Result<DateTimeOffset>
                .Failure(GenericErrors.NoRecordFound(nameof(InterviewTask), request.TaskId));

        var result = task.Rescheduling(request.InterviewDate);
        if (result.IsFailure)
            return Result<DateTimeOffset>.Failure(result.Errors);

        await _uow.SaveChangesAsync();

        return Result<DateTimeOffset>.Success(request.InterviewDate);
    }
}
