using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;

namespace InterviewSystem.Application.Employees.GetEmployeeDetails;

public sealed class Handler
{
    private readonly IEmployeeRepository _employeeRepository;
    public Handler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Result<EmployeeDetailsDTO>> Handle(GetEmployeeDetailsQuery query)
    {
        if (query.EmployeeId == Guid.Empty)
            return Result<EmployeeDetailsDTO>.Failure(GenericErrors.Required(nameof(query.EmployeeId)));

        var employee = await _employeeRepository.GetByIdAsync(query.EmployeeId);

        if (employee is null)
            return Result<EmployeeDetailsDTO>.Failure(GenericErrors.NotFound(nameof(Employee), query.EmployeeId));

        var dto = MapToDTO(employee);

        return Result<EmployeeDetailsDTO>.Success(dto);
        
    }

    private EmployeeDetailsDTO MapToDTO(Employee employee)
    {
        return new EmployeeDetailsDTO(
            employee.Id,
            employee.Name!,
            employee.Email!,
            employee.EmployeeType,
            employee.EmployeeDepartment,
            employee.EmployeePosition,
            employee.EmployeeStatus
            );
    }

}
