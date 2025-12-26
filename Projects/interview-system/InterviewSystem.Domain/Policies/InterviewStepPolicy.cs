using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Domain.Policies;

public sealed class InterviewStepPolicy
{
    // Who is allowed to perform this step
    public EmployeeDepartment InterviewerDepartment { get; init; }
    public IReadOnlySet<EmployeePosition> AllowedPositions { get; init; }

    // Structural semantics
    public bool IsMandatory { get; init; }
    public bool CanCompleteProcess { get; init; }
    // Multiplicity
    public bool AllowMultiple { get; init; }
    // Optional but strongly recommended
    public int Sequence { get; init; }
}
