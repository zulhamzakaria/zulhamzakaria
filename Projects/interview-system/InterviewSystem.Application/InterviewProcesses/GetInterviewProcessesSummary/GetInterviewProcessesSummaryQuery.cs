using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Application.InterviewProcesses.GetInterviewProcessesSummary;

public sealed record GetInterviewProcessesSummaryQuery(EmployeeDepartment? EmployeeDepartment,
    InterviewProcessStatus? InterviewProcessStatus,
    Guid? InterviewerId);
