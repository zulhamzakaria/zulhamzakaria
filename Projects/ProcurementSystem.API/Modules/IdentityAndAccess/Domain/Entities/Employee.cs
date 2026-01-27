using ProcurementSystem.API.SharedKernel;
using ProcurementSystem.API.SharedKernel.ErrorHandling;
using ProcurementSystem.API.SharedKernel.ErrorHandling.Errors;
using ProcurementSystem.API.SharedKernel.Infrastructure;

namespace ProcurementSystem.API.Modules.IdentityAndAccess.Domain.Entities;

public sealed class Employee : BaseEntity, ITenantEntity
{
    private const int MinNameLength = 3;
    private const int MaxNameLength = 100;
    private const int MinEmailLength = 5;
    private const int MaxEmailLength = 100;

    public Guid TenantId { get; private set; }

    Guid ITenantEntity.TenantId
    {
        get => TenantId;
        set => TenantId = value;
    }
    public string EmployeeName { get; private set; } = string.Empty;
    public string EmployeeNumber { get; private set; } = string.Empty;
    public string EmployeeEmail { get; private set; } = string.Empty;

    public Guid? UserId { get; private set; }

    public Employee()
    {
        // EF Core
    }

    public Result<Employee> Create(Guid tenantId, string employeeName, string employeeNumber, string employeeEmail)
    {
        List<Error> errors = new();
        if(tenantId == Guid.Empty)
            errors.Add(CommonErrors.InvalidInput(nameof(tenantId)));
        if (string.IsNullOrWhiteSpace(employeeName))
            errors.Add(CommonErrors.Required(nameof(employeeName)));
        else if (employeeName.Length < MinNameLength || employeeName.Length > MaxNameLength)
            errors.Add(CommonErrors.InvalidLength(nameof(employeeName), MinNameLength, MaxNameLength));
        if (string.IsNullOrWhiteSpace(employeeNumber))
            errors.Add(CommonErrors.Required(nameof(employeeNumber)));
        if (string.IsNullOrWhiteSpace(employeeEmail))
            errors.Add(CommonErrors.Required(nameof(employeeEmail)));
        else if (employeeEmail.Length < MinEmailLength || employeeEmail.Length > MaxEmailLength)
            errors.Add(CommonErrors.InvalidLength(nameof(employeeEmail), MinEmailLength, MaxEmailLength));
        if (errors.Any())
            return Result<Employee>.Failure(errors);
        var employee = new Employee
        {
            EmployeeName = employeeName,
            EmployeeNumber = employeeNumber,
            EmployeeEmail = employeeEmail
        };
        return Result<Employee>.Success(employee);
    }
}
