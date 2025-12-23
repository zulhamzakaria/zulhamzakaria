using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Application.Employees.GetEmployeesSummary;

public sealed record GetEmployeeSummaryDTO(
    Guid Id,
    string Name,
    EmployeeDepartment EmployeeDepartment,
    EmployeePosition EmployeePosition,
    EmployeeStatus EmployeeStatus
    );
