using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Application.Employees.GetEmployeesSummary;

public sealed record GetEmployeesSummaryQuery(
    EmployeeType? EmployeeType = null,
    EmployeeDepartment? EmployeeDepartment = null,
    EmployeePosition? EmployeePosition = null,
    EmployeeStatus? EmployeeStatus = null
    );
