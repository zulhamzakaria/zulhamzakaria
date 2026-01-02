using InterviewSystem.Domain.Common.Enums;
using InterviewSystem.Domain.Entity;

namespace InterviewSystem.Application.InterviewTasks.GetInterviewTaskDetails;

public sealed record GetInterviewTaskDetailsDTO(
    Guid Id,
    Guid InterviewProcessId,
    Guid CandidateId,
    string CandidateName,
    Guid AssigneeId,
    InterviewTaskStatus InterviewTaskStatus,
    DateTimeOffset AssignedAt,
    DateTimeOffset? InterviewDate,
    DateTimeOffset? CompletedAt,
    CandidateEvaluation? CandidateEvaluation,
    bool Rejected,
    string? TaskRejectionReason
    );
