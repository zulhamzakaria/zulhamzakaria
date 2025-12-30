using InterviewSystem.Domain.Common.Enums;
using InterviewSystem.Domain.Entity;

namespace InterviewSystem.Domain.Interfaces.Repositories;

public interface IEmployeeRepository
{
    Task<IReadOnlyCollection<Employee>> GetAllAsync();
    Task<IReadOnlyCollection<Employee>> GetEmployeesByPositionAsync(EmployeePosition position, EmployeeDepartment department);
    Task<Employee?> GetByIdAsync(Guid id);
    Task AddAsync(Employee employee);
}
