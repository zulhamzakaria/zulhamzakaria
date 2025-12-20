using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Application.InterviewProcesses.GetInterviewProcessesSummary;

public sealed record Query(EmployeeDepartment? EmployeeDepartment,
    InterviewProcessStatus? InterviewProcessStatus,
    Guid? InterviewerId);
