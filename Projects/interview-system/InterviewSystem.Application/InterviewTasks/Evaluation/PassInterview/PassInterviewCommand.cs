using InterviewSystem.Domain.Common.ErrorHandling;
using MediatR;

namespace InterviewSystem.Application.InterviewTasks.Evaluation.CompleteInterview;

public sealed record PassInterviewCommand(
    Guid TaskId,
    Guid AssigneeId,
    string? Reason) : IRequest<Result<Guid>>;
