using InterviewSystem.Domain.Common;
using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;
using InterviewSystem.Domain.Interfaces.Repositories;

namespace InterviewSystem.Application.Employees.DeactivateEmployee;

public sealed class Handler
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUnitOfWorkRepository _uow;

    public Handler(IEmployeeRepository employeeRepository, IUnitOfWorkRepository uow)
    {
        _employeeRepository = employeeRepository;
        _uow = uow;
    }

    public async Task<Result<Unit>> HandleAsync(DeactivateEmployeeCommand command)
    {
        var employee = await _employeeRepository.GetByIdAsync(command.employeeId);
        if (employee is null)
            return Result<Unit>.Failure(GenericErrors.NotFound(nameof(command.employeeId)));

        employee.Deactivate();
        await _uow.SaveChangesAsync();
        return Result<Unit>.Success(Unit.Value);
    }

}
