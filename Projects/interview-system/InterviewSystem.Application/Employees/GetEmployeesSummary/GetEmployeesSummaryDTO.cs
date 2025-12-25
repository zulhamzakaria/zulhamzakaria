using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Application.Employees.GetEmployeesSummary;

public sealed record GetEmployeesSummaryDTO(
    Guid Id,
    string Name,
    EmployeeDepartment EmployeeDepartment,
    EmployeePosition EmployeePosition,
    EmployeeStatus EmployeeStatus
    );
