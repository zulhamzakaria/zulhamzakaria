using InterviewSystem.Domain.Common.ErrorHandling;
using MediatR;

namespace InterviewSystem.Application.InterviewTasks.Scheduling.SetInterviewDate;

public sealed record SetInterviewDateCommand(
    Guid TaskId,
    Guid AssigneeId,
    DateTimeOffset InterviewDate) : IRequest<Result<DateTimeOffset>>;
