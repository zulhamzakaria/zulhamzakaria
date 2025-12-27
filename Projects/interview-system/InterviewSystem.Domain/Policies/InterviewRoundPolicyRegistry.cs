using InterviewSystem.Domain.Common.Enums;
using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;

namespace InterviewSystem.Domain.Policies;

public class InterviewRoundPolicyRegistry
{
    private static readonly IReadOnlyDictionary<AppliedPosition, InterviewRoundPolicy> _policies
         = new Dictionary<AppliedPosition, InterviewRoundPolicy>
         {
             [AppliedPosition.TeaLady] = HRInterviewRoundPolicy.TeaLadyPolicy,
             [AppliedPosition.Clerk] = HRInterviewRoundPolicy.ClerkPolicy,
             [AppliedPosition.JuniorEngineer] = EngineeringInterviewRoundPolicy.JuniorEngineerPolicy,
             [AppliedPosition.SeniorEngineer] = EngineeringInterviewRoundPolicy.SeniorEngineerPolicy,
             [AppliedPosition.TechnicalLead] = EngineeringInterviewRoundPolicy.TechnicalLead,
         };

    public static Result<InterviewRoundPolicy> GetPolicy(AppliedPosition position)
    {
        if (_policies.TryGetValue(position, out var policy) is false)
            return Result<InterviewRoundPolicy>.Failure(InterviewRoundErrors.UndefinedPolicy(position));

        return Result<InterviewRoundPolicy>.Success(policy);
    }
}
