using InterviewSystem.Domain.Common.Enums;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InterviewSystem.Infrastructure.Repositories;

public class EmployeeRepository: IEmployeeRepository
{
    private readonly AppDbContext _context;
    public EmployeeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Employee employee)
    {
        await _context.Employees.AddAsync(employee);
    }

    public async Task<IReadOnlyCollection<Employee>> GetAllAsync()
    {
        return await _context.Employees.ToListAsync();
    }

    public async Task<Employee?> GetByIdAsync(Guid id)
    {
        return await _context.Employees.FindAsync(id);
    }

    public async Task<IReadOnlyCollection<Employee>> GetEmployeesByPositionAsync
        (EmployeePosition position)
    {
        return await _context.Employees
            .Where(e => e.EmployeePosition == position)
            .Where(e => e.EmployeeStatus == EmployeeStatus.Active)
            .ToListAsync();
    }
}
