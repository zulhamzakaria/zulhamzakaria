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

    public async Task<Result<Guid>> Handle(DeactivateEmployeeCommand command)
    {
        var employee = await _employeeRepository.GetByIdAsync(command.employeeId);
        if (employee is null)
            return Result<Guid>.Failure(GenericErrors.NotFound(nameof(command.employeeId)));

        employee.
    }

}
