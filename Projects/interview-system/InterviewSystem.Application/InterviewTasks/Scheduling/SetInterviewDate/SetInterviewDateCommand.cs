using InterviewSystem.Domain.Common.ErrorHandling;
using MediatR;

namespace InterviewSystem.Application.InterviewTasks.Scheduling.SetInterviewDate;

public sealed record SetInterviewDateCommand(
    Guid AssigneeId,
    Guid TaskId,
    DateTimeOffset InterviewDate) : IRequest<Result<DateTimeOffset>>;
