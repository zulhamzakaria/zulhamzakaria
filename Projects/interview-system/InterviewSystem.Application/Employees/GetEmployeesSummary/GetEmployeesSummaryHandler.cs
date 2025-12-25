using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace InterviewSystem.Application.Employees.GetEmployeesSummary;

public sealed class GetEmployeesSummaryHandler : 
    IRequestHandler<GetEmployeesSummaryQuery, Result< IReadOnlyCollection< GetEmployeesSummaryDTO>>>
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetEmployeesSummaryHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Result<IReadOnlyCollection<GetEmployeesSummaryDTO>>> Handle
        (GetEmployeesSummaryQuery request, CancellationToken cancellationToken)
    {
        var employees = await _employeeRepository.GetAllAsync();

        var filteredEmployees = employees
            .Where(e => request.EmployeeStatus == null || e.EmployeeStatus == request.EmployeeStatus)
            .Where(e => request.EmployeeType == null || e.EmployeeType == request.EmployeeType)
            .Where(e => request.EmployeePosition == null || e.EmployeePosition == request.EmployeePosition)
            .Where(e => request.EmployeeDepartment == null || e.EmployeeDepartment == request.EmployeeDepartment)
            .Select(MapToDto)
            .ToList();

        if (filteredEmployees.Any() is false)
            return Result<IReadOnlyCollection<GetEmployeesSummaryDTO>>.Failure(GenericErrors.NoRecordsFound(nameof(Employee)));

        return Result<IReadOnlyCollection<GetEmployeesSummaryDTO>>.Success(filteredEmployees);

    }

    private GetEmployeesSummaryDTO MapToDto(Employee employee)
    {
        return new GetEmployeesSummaryDTO(
            employee.Id,
            employee.Name!,
            employee.EmployeeDepartment,
            employee.EmployeePosition,
            employee.EmployeeStatus
            );
    }

}
