using InterviewSystem.Domain.Common.Enums;
using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;

namespace InterviewSystem.Domain.Entity;

public class InterviewRoundItem
{
    public int Sequence { get; private set; }
    public EmployeePosition AllowedPosition { get; private set; }
    public bool IsMandatory { get; private set; } 
    public bool CanCompleteProcess { get; private set; } 
    public bool AllowMultiple { get; private set; } 

    private InterviewRoundItem()
    {
        //EF Core needs this
    }

   public static Result<InterviewRoundItem> Create(int sequence, EmployeePosition position,
       bool isMandatory, bool canCompleteProcess, bool allowMultiple)
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
            AllowedPosition = position,
            IsMandatory = isMandatory,
            CanCompleteProcess = canCompleteProcess,
            AllowMultiple = allowMultiple
        };

        return Result<InterviewRoundItem>.Success(item);
    }
}
