using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Application.InterviewTasks.GetInterviewTasksSummary;

public sealed record GetInterviewTasksSummaryQuery(
        EmployeeDepartment? EmployeeDepartment,
        AppliedPosition? AppliedPosition,
        InterviewTaskStatus? InterviewTaskStatus,
        bool? Rejected,
        string? TaskRejectionReason
        );
