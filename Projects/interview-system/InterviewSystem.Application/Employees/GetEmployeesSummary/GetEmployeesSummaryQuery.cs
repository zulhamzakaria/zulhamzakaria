using InterviewSystem.Domain.Common.Enums;
using InterviewSystem.Domain.Common.ErrorHandling;
using MediatR;

namespace InterviewSystem.Application.Employees.GetEmployeesSummary;

public sealed record GetEmployeesSummaryQuery(
    EmployeeType? EmployeeType = null,
    EmployeeDepartment? EmployeeDepartment = null,
    EmployeePosition? EmployeePosition = null,
    EmployeeStatus? EmployeeStatus = null
    ) : IRequest<Result<IReadOnlyCollection<GetEmployeesSummaryDTO>>>;
