using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Application.DTOs.Employee;

public record EmployeeDetailsDTO(
    Guid Id,
    string Name,
    string Email,
    EmployeeType EmployeeType,
    EmployeeDepartment EmployeeDepartment,
    EmployeePosition EmployeePosition,
    EmployeeStatus EmployeeStatus
    );
