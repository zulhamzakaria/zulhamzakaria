using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Application.Employees.GetEmployeesSummary;

public sealed record GetEmployeesSummaryQuery(EmployeeDepartment? Department);
