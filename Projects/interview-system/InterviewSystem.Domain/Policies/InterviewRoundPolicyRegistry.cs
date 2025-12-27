using InterviewSystem.Domain.Common.Enums;

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

    public static InterviewRoundPolicy GetPolicy(AppliedPosition position)
        => _policies[position];
}
