using InterviewSystem.Domain.Common.Enum;

namespace InterviewSystem.Domain.Entity;

public class Employee
{
    public Guid Id { get; private set; }
    public string? Name { get; private set; }
    public string? Email { get; private set; }
    public EmployeeType EmployeeType { get; private set; }
    public EmployeeDepartment EmployeeDepartment { get; private set; }
    public EmployeePosition EmployeePosition { get; private set; }
    public EmployeeStatus EmployeeStatus { get; private set; }

    public Employee(){} //required by EF Core

    public Employee(Guid id, string name, string email,
        EmployeeType type, EmployeeDepartment department, 
        EmployeePosition position, EmployeeStatus status)
    {
        Id = id;
        Name = name;
        Email = email;
        EmployeeType = type;
        EmployeeDepartment = department;
        EmployeePosition = position;
        EmployeeStatus = status;
    }
}
