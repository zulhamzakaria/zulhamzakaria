using InterviewSystem.Domain.Entity;

namespace InterviewSystem.Domain.Interfaces.Repositories;

public interface IEmployeeRepository
{
    Task<IReadOnlyCollection<Employee>> GetAllAsync();
    Task<Employee?> GetByIdAsync(Guid id);
    Task AddAsync(Employee employee);
}
