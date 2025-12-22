using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Application.InterviewTasks.GetInterviewTasksSummary;

public sealed record DTO(
    Guid Id,
    string CandidateName,
    EmployeeDepartment EmployeeDepartment,
    AppliedPosition AppliedPosition,
    InterviewTaskStatus InterviewTaskStatus,
    DateTimeOffset AssignedAt,
    bool Rejected,
    string? TaskRejectionReason
    );
