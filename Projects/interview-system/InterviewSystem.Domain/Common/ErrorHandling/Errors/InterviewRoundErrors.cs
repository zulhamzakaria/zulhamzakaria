using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Domain.Common.ErrorHandling.Errors;

public static class InterviewRoundErrors
{
    public static Error UndefinedPolicy(AppliedPosition appliedPosition)
        => new(ErrorType.Validation, "POLICY_NOT_DEFINED", 
            $"No InterviewRound Policy has been defined for {appliedPosition.ToString()}");
    public static Error UndefinedInitiator()
        => new(ErrorType.Validation, "INITIATOR_NOT_DEFINED",
            $"No Initiator has been defined for the Policy");
}
