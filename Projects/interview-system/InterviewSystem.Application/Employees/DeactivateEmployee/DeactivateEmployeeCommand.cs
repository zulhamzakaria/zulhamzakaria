using InterviewSystem.Domain.Common.ErrorHandling;
using MediatR;
using Unit = InterviewSystem.Domain.Common.Unit;

namespace InterviewSystem.Application.Employees.DeactivateEmployee;

public sealed record DeactivateEmployeeCommand(Guid employeeId) : IRequest<Result<Unit>>;
