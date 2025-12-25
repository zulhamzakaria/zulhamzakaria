using InterviewSystem.Domain.Common.ErrorHandling;
using MediatR;

namespace InterviewSystem.Application.Employees.GetEmployeeDetails;

public sealed record GetEmployeeDetailsQuery(Guid EmployeeId): IRequest<Result<EmployeeDetailsDTO>>;

