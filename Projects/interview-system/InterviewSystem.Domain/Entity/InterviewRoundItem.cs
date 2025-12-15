using InterviewSystem.Domain.Common.Enums;
using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;

namespace InterviewSystem.Domain.Entity;

public class InterviewRoundItem
{
    public int Sequence { get; private set; }
    public EmployeePosition Position { get; private set; }

    private InterviewRoundItem()
    {
        //EF Core needs this
    }

    //private InterviewRoundItem(int sequence, EmployeePosition employee)
    //{
    //    Sequence = sequence;
    //    Position = employee;
    //}

   public static Result<InterviewRoundItem> Create(int sequence, EmployeePosition position)
    {
        List<Error> errors = new();

        if(sequence <= 0)
        {
            errors.Add(GenericErrors.InvalidIntValue(nameof(sequence)));
        }
        if(Enum.IsDefined(position) is false)
        {
            errors.Add(GenericErrors.InvalidEnumValue(position));
        }

        if (errors.Any())
        {
            return Result<InterviewRoundItem>.Failure(errors);
        }

        InterviewRoundItem item = new()
        {
            Sequence = sequence,
            Position = position
        };

        return Result<InterviewRoundItem>.Success(item);
    }
}
