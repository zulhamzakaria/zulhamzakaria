using InterviewSystem.Domain.Common.Enums;
using InterviewSystem.Domain.Entity;

namespace InterviewSystem.Application.InterviewTasks.GetInterviewTaskDetails;

public sealed record DTO(
    Guid Id,
    Guid InterviewRoundId,
    Guid CandidateId,
    string CandidateName,
    InterviewTaskStatus InterviewTaskStatus,
    DateTimeOffset AssignedAt,
    DateTimeOffset? CompletedAt,
    CandidateEvaluation? CandidateEvaluation,
    bool Rejected,
    string? TaskRejectionReason
    );
