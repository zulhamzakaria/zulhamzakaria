using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Application.Employees.CreateEmployee;

public sealed record CreateEmployeeRequestDto(
    string Name,
    string Email,
    EmployeeType EmployeeType,
    EmployeeDepartment EmployeeDepartment,
    EmployeePosition EmployeePosition
);
