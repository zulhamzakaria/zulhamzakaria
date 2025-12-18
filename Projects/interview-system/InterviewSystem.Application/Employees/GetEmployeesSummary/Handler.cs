using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;

namespace InterviewSystem.Application.Employees.GetEmployeesSummary;

public sealed class Handler
{
    private readonly IEmployeeRepository _employeeRepository;

    public Handler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Result<IReadOnlyCollection<EmployeeSummaryDTO>>> Handle(GetEmployeesSummaryQuery query)
    {
        var employees = await _employeeRepository.GetAllAsync();

        if (employees.Any() is false)
            return Result<IReadOnlyCollection<EmployeeSummaryDTO>>
                .Failure(GenericErrors.NotFound(nameof(Employee)));

        var dtos = employees.Select(MapToDto).ToList();
        
        return Result<IReadOnlyCollection<EmployeeSummaryDTO>>.Success(dtos); 

    }

    private EmployeeSummaryDTO MapToDto(Employee employee)
    {
        return new EmployeeSummaryDTO(
            employee.Id,
            employee.Name!,
            employee.EmployeeDepartment,
            employee.EmployeePosition,
            employee.EmployeeStatus
            );
    }

}
