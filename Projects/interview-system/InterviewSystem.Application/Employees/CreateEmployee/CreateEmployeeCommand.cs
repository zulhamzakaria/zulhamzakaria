using InterviewSystem.Domain.Common.Enums;
using InterviewSystem.Domain.Common.ErrorHandling;
using MediatR;

namespace InterviewSystem.Application.Employees.CreateEmployee;

public sealed record CreateEmployeeCommand(
    string Name,
    string Email,
    EmployeeDepartment EmployeeDepartment,
    EmployeePosition EmployeePosition
    ) : IRequest<Result<Guid>>;
