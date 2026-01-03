using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Application.InterviewTasks.GetInterviewTasksSummary;
public sealed record GetInterviewTasksSummaryDTO(
    Guid InterviewProcessId,
    Guid InterviewTaskId,
    string CandidateName,
    EmployeeDepartment EmployeeDepartment,
    AppliedPosition AppliedPosition,
    InterviewTaskStatus InterviewTaskStatus,
    DateTimeOffset AssignedAt,
    Guid? RejectionOriginatorId,
    bool Rejected,
    string? TaskRejectionReason
    );
