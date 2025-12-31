using InterviewSystem.Domain.Common.Enums;
using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;
using InterviewSystem.Domain.Policies;

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

    public static Result<InterviewRound> Create(EmployeeDepartment employeeDepartment,
        AppliedPosition appliedPosition, IReadOnlyList<InterviewStepPolicy> steps)
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

        var round = new InterviewRound()
        {
            Id = Guid.NewGuid(),
            AppliedPosition = appliedPosition,
            Department = employeeDepartment,
        };

        //Create RoundItem
        foreach(var step in steps)
        {
            var roundItem = InterviewRoundItem.Create(
                step.Sequence,
                step.AllowedPositions.FirstOrDefault(),
                step.IsMandatory,
                step.CanCompleteProcess,
                step.AllowMultiple);

            if (roundItem.IsFailure)
            {
                errors.AddRange(roundItem.Errors);
                continue;
            }

            round._items.Add(roundItem.Value!);
        }

        if (errors.Any())
        {
            return Result<InterviewRound>.Failure(errors);
        }

        return Result<InterviewRound>.Success(round);
    }

}
