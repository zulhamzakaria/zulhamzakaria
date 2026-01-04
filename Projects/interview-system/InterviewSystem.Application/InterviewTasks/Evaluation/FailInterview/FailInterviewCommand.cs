using InterviewSystem.Domain.Common.ErrorHandling;
using MediatR;

namespace InterviewSystem.Application.InterviewTasks.Evaluation.FailInterview;

public sealed record FailInterviewCommand(
    Guid TaskId,
    Guid AssigneeId,
    string Reason) : IRequest<Result<Guid>>;

