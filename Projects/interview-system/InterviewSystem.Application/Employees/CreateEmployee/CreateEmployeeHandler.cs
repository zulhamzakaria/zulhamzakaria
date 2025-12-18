using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;

namespace InterviewSystem.Application.Employees.CreateEmployee;

public sealed class CreateEmployeeHandler
{
    private readonly IUnitOfWorkRepository _uow;
    private readonly IEmployeeRepository _employeeRepository;
    public CreateEmployeeHandler(IUnitOfWorkRepository uow, IEmployeeRepository employeeRepository)
    {
        _uow = uow;
        _employeeRepository = employeeRepository;
    }

    public async Task<Result<Guid>> HandleAsync(CreateEmployeeCommand command)
    {
        var employeeResult = Employee.Create(
            command.Name,
            command.Email,
            command.EmployeeType,
            command.EmployeeDepartment,
            command.EmployeePosition
            );
    }
}
