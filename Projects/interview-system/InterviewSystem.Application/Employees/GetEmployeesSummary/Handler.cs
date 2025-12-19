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

    public async Task<Result<IReadOnlyCollection<EmployeeSummaryDTO>>> HandleAsync(GetEmployeesSummaryQuery query)
    {
        var employees = await _employeeRepository.GetAllAsync();

        var filteredEmployees = employees
            .Where(e => query.EmployeeStatus == null || e.EmployeeStatus == query.EmployeeStatus)
            .Where(e => query.EmployeeType == null || e.EmployeeType == query.EmployeeType)
            .Where(e => query.EmployeePosition == null || e.EmployeePosition == query.EmployeePosition)
            .Where(e => query.EmployeeDepartment == null || e.EmployeeDepartment == query.EmployeeDepartment)
            .Select(MapToDto)
            .ToList();
        
        return Result<IReadOnlyCollection<EmployeeSummaryDTO>>.Success(filteredEmployees); 

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
