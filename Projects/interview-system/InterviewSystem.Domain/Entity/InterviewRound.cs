using InterviewSystem.Domain.Common.Enums;
using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;

namespace InterviewSystem.Domain.Entity;

public class InterviewRound : EntityBase
{
    public Guid Id { get; private set; }
    public EmployeeDepartment Department { get; private set; }
    public AppliedPosition AppliedPosition { get; private set; }

    private readonly List<InterviewRoundItem> _items = new();
    public IReadOnlyList<InterviewRoundItem> Items => _items.AsReadOnly();
    public int NumberOfRounds => _items.Count(); //get number of InterviewRoundItems

    private InterviewRound()
    {
        //EF Core needs this
    }

    //private InterviewRound(Guid id, EmployeeDepartment department, List<InterviewRoundItem> items)
    //{
    //    Id = id;
    //    Department = department;
    //    _items = items.OrderBy(i => i.Sequence).ToList();
    //}

    public static Result<InterviewRound> Create(EmployeeDepartment employeeDepartment,
        AppliedPosition appliedPosition, List<EmployeePosition> positions)
    {

        List<Error> errors = new();

        if (Enum.IsDefined(employeeDepartment) is false)
        {
            errors.Add(GenericErrors.InvalidEnumValue(employeeDepartment));
        }
        if (Enum.IsDefined(appliedPosition) is false)
        {
            errors.Add(GenericErrors.InvalidEnumValue(appliedPosition));
        }
        if (positions is null || positions.Any() is false)
        {
            errors.Add(GenericErrors.Required(nameof(positions)));
        }

        if (errors.Any())
        {
            return Result<InterviewRound>.Failure(errors);
        }

        var round = new InterviewRound()
        {
            Id = Guid.NewGuid(),
            AppliedPosition = appliedPosition,
            Department = employeeDepartment,
        };

        int sequence = 0;
        foreach (var post in positions!)
        {
            var item = InterviewRoundItem.Create(sequence + 1, post);
            if (item.IsFailure)
            {
                return Result<InterviewRound>.Failure(item.Errors);
            }
            round._items.Add(item.Value!);
            sequence++;
        }
        return Result<InterviewRound>.Success(round);
    }

}
