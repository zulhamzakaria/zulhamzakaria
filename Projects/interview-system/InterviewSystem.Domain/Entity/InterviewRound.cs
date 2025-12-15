using InterviewSystem.Domain.Common.Enums;
using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;
using System.Runtime.CompilerServices;

namespace InterviewSystem.Domain.Entity;

public class InterviewRound : EntityBase
{
    public Guid Id { get; private set; }
    public EmployeeDepartment Department { get; private set; }
    public int NumberOfRounds { get; private set; } //get number of InterviewRoundItems

    private readonly List<InterviewRoundItem> _items = new();
    public IReadOnlyList<InterviewRoundItem> Items => _items.AsReadOnly();

    private InterviewRound()
    {
        //EF Core needs this
    }

    private InterviewRound(Guid id, EmployeeDepartment department, List<InterviewRoundItem> items)
    {
        Id = id;
        Department = department;
        _items = items.OrderBy(i => i.Sequence).ToList();
        NumberOfRounds = items.Count;
    }

    public static Result<InterviewRound> Create(EmployeeDepartment employeeDepartment, List<Employee> employees)
    {

        List<Error> errors = new();

        if (Enum.IsDefined(employeeDepartment) is false)
        {
            errors.Add(GenericErrors.InvalidEnumValue(employeeDepartment));
        }
        if (employees is null || employees.Any() is false)
        {
            errors.Add(GenericErrors.Required(nameof(employees)));
        }

        if (errors.Any())
        {
            return Result<InterviewRound>.Failure(errors);
        }


        var round = new InterviewRound()
        {
            Id = Guid.NewGuid(),
            Department = employeeDepartment,
            NumberOfRounds = 10
        };

        return Result<InterviewRound>.Success(round);   
    }

}
