using InterviewSystem.Domain.Common.ErrorHandling;
using MediatR;

namespace InterviewSystem.Application.InterviewTasks.Evaluation.CompleteInterview;

public sealed record CompleteInterviewCommand(
    Guid TaskId,
    Guid AssigneeId,
    string? Reason) : IRequest<Result<Guid>>;
