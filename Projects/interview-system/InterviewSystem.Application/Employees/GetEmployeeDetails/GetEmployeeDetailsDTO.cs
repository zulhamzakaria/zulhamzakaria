using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Application.Employees.GetEmployeeDetails;

public sealed record EmployeeDetailsDTO(
    Guid Id,
    string Name,
    string Email,
    EmployeeType EmployeeType,
    EmployeeDepartment EmployeeDepartment,
    EmployeePosition EmployeePosition,
    EmployeeStatus EmployeeStatus
    );
