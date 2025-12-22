using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Application.InterviewRounds.GetInterviewRoundsSummary;

public sealed record Query(
    EmployeeDepartment? EmployeeDepartment,
    AppliedPosition? AppliedPosition
    );
