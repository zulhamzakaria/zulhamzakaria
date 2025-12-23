using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Infrastructure.CustomQueries.InterviewTasks.DTOs;

public sealed record InterviewTasksSummaryDTO(
    Guid Id,
    string CandidateName,
    EmployeeDepartment EmployeeDepartment,
    AppliedPosition AppliedPosition,
    InterviewTaskStatus InterviewTaskStatus,
    DateTimeOffset AssignedAt,
    bool Rejected,
    string? TaskRejectionReason
    );
