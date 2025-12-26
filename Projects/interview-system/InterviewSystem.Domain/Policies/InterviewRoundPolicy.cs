using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Domain.Policies;

public sealed class InterviewRoundPolicy
{
    public EmployeeDepartment AppliedDepartment { get; init; }
    public IReadOnlyList<InterviewStepPolicy> Steps { get; init; }
}
