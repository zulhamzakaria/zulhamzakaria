using InterviewSystem.Domain.Common.ErrorHandling;
using MediatR;

namespace InterviewSystem.Application.InterviewTasks.Scheduling.RejectTask;

public sealed record RejectTaskCommand(
    Guid TaskId,
    Guid AssigneedId) : IRequest<Result<Guid>>;
