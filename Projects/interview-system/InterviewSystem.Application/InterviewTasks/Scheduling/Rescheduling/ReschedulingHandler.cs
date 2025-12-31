using InterviewSystem.Domain.Common.Enums;
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
        var task = await _interviewTaskRepository.GetByIdAsync(request.TaskId);
        if (task is null)
            return Result<DateTimeOffset>
                .Failure(GenericErrors.NoRecordFound(nameof(InterviewTask), request.TaskId));

        var isOwner = await _interviewTaskRepository.IsAssigneeOwnerAsync(request.TaskId, request.AssigneeId);
        if (isOwner is false)
            return Result<DateTimeOffset>
                .Failure(InterviewTaskErrors.InvalidAction(request.AssigneeId, request.TaskId));

        //cannot schedule at the same time as other Task
        var tasks = (await _interviewTaskRepository.GetAllByEmployeeIdAsync(request.AssigneeId))
            .Where(it => it.InterviewTaskStatus is InterviewTaskStatus.Accepted)
            .ToList();
        
        bool exist = tasks.Any(it => it.InterviewDate == request.InterviewDate);
        if (exist)
            return Result<DateTimeOffset>.Failure(InterviewTaskErrors.TimeSlotTaken());

        var periodStart = request.InterviewDate.AddHours(-1); //10am -> 9am
        var periodEnd = request.InterviewDate.AddHours(1); //10am -> 11am

        //task.Interview date occupies 9am - 10am space && 10 am - 11am
        //valid period is 9am (Valid): 10am (Valid): 11am(Valid)
        var hasConflict = tasks.Any
            (it => it.InterviewDate >= periodStart && it.InterviewDate < periodEnd);
        if (hasConflict)
            return Result<DateTimeOffset>.Failure(InterviewTaskErrors.TimeSlotTaken());


        var result = task.Rescheduling(request.InterviewDate);
        if (result.IsFailure)
            return Result<DateTimeOffset>.Failure(result.Errors);

        await _uow.SaveChangesAsync();

        return Result<DateTimeOffset>.Success(request.InterviewDate);
    }
}
