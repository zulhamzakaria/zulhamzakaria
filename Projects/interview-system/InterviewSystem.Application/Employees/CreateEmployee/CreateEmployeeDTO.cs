using InterviewSystem.Domain.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace InterviewSystem.Application.Employees.CreateEmployee;

public sealed record CreateEmployeeRequestDto(
    [Required, StringLength(100, MinimumLength = 1)] string Name,
    [Required, StringLength(100, MinimumLength = 1), EmailAddress] string Email,
    [Required] EmployeeType EmployeeType,
    [Required] EmployeeDepartment EmployeeDepartment,
    [Required] EmployeePosition EmployeePosition
);
