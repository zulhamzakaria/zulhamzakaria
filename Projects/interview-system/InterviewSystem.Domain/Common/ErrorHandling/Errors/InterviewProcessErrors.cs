using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Domain.Common.ErrorHandling.Errors;

public static class InterviewProcessErrors
{
    public static Error CannotAdvance()
        => new(ErrorType.BusinessRule, "COMPLETED_INTERVIEW_PROCESS", 
            "Cannot advance a completed InterviewProcess");
    public static Error InvalidStatusChange()
        => new(ErrorType.BusinessRule, "COMPLETED_INTERVIEW_PROCESS", 
            "Cannot modify a completed InterviewProcess");
    public static Error InvalidSequence()
        => new(ErrorType.BusinessRule, "PROCESS_SEQUENCE_INVALID", 
            "Next sequence cannot be lower than the current sequence");
    public static Error UndefinedDepartment(AppliedPosition appliedPosition)
            => new(ErrorType.Validation, "DEPARTMENT_NOT_DEFINED", 
                $"No Department defined for this AppliedPosition:{appliedPosition.ToString()}");
}
