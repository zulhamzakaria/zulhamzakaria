using InterviewSystem.Domain.Common;
using InterviewSystem.Domain.Common.Enums;
using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;

namespace InterviewSystem.Domain.Entity;

public class Employee : EntityBase
{
    private const int MinLength = 1;
    private const int MaxNameLength = 100;
    private const int MaxEmailLength = 100;

    public Guid Id { get; private set; }
    public string? Name { get; private set; }
    public string? Email { get; private set; }

    public EmployeeDepartment EmployeeDepartment { get; private set; }
    public EmployeePosition EmployeePosition { get; private set; }
    public EmployeeStatus EmployeeStatus { get; private set; }
    public int EmployeeLevel { get; private set; }

    public bool CanInterview => EmployeePosition != EmployeePosition.Clerk;
    public bool CanCoordinate => EmployeePosition == EmployeePosition.Clerk;

    public Employee() { } //required by EF Core

    //public Employee(Guid id, string name, string email,
    //    EmployeeType type, EmployeeDepartment department, 
    //    EmployeePosition position, EmployeeStatus status)
    //{
    //    Id = id;
    //    Name = name;
    //    Email = email;
    //    EmployeeType = type;
    //    EmployeeDepartment = department;
    //    EmployeePosition = position;
    //    EmployeeStatus = status;
    //}

    public static Result<Employee> Create(string name, string email,
        EmployeeDepartment department, EmployeePosition position)
    {

        List<Error> errors = new();

        if (string.IsNullOrWhiteSpace(name))
            errors.Add(GenericErrors.Required(nameof(name)));
        if (string.IsNullOrWhiteSpace(email))
            errors.Add(GenericErrors.Required(nameof(email)));
        if (name.Length > MaxNameLength)
            errors.Add(GenericErrors.InvalidLength(nameof(name), MinLength, MaxNameLength));
        if (email.Length > MaxEmailLength)
            errors.Add(GenericErrors.InvalidLength(nameof(name), MinLength, MaxEmailLength));

        if (Enum.IsDefined<EmployeeDepartment>(department) is false)
            errors.Add(GenericErrors.InvalidEnumValue(department));
        if (Enum.IsDefined<EmployeePosition>(position) is false)
            errors.Add(GenericErrors.InvalidEnumValue(position));


        var result = EmployeeRules.IsPositionValidForDepartment(department, position);
        if (result.IsFailure)
        {
            errors.Add(EmployeeErrors.InvalidDepartment(department));
        }
        else if (result.Value is false)
        {
            errors.Add(EmployeeErrors.InvalidEmployeeForDepartment(department, position));
        }

        if (errors.Any())
            return Result<Employee>.Failure(errors);

        var employee = new Employee()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Email = email,
            EmployeeDepartment = department,
            EmployeePosition = position,
            EmployeeStatus = EmployeeStatus.Active
        };

        return Result<Employee>.Success(employee);
    }

    public Result<Unit> Deactivate()
    {
        if (EmployeeStatus == EmployeeStatus.Inactive)
            return Result<Unit>.Failure(EmployeeErrors.InvalidEmployeeStatus());

        EmployeeStatus = EmployeeStatus.Inactive;
        return Result<Unit>.Success(Unit.Value);
    }
}
