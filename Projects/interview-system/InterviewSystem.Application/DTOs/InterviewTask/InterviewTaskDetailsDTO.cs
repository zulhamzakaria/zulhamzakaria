using InterviewSystem.Application.DTOs.CandidateEvaluation;
using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Application.DTOs.InterviewTask;

public record InterviewTaskDetailsDTO(
    Guid Id,
    Guid InterviewRoundId,
    Guid CandidateId,
    InterviewTaskStatus InterviewTaskStatus,
    DateTimeOffset AssignedAt,
    DateTimeOffset? CompletedAt,
    CandidateEvaluationDetailsDTO? CandidateEvaluation,
    bool Rejected,
    string TaskRejectionReason
    );
