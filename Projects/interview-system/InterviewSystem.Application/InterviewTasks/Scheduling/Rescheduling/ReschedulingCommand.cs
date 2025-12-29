using InterviewSystem.Domain.Common.ErrorHandling;
using MediatR;

namespace InterviewSystem.Application.InterviewTasks.Scheduling.Rescheduling;

public sealed record ReschedulingCommand(
    Guid TaskId, 
    Guid AssigneeId,
    DateTimeOffset InterviewDate): IRequest<Result<DateTimeOffset>> ;
