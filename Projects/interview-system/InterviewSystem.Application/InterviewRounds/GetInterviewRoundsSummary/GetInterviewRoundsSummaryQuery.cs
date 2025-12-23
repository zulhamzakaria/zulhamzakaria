using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Application.InterviewRounds.GetInterviewRoundsSummary;

public sealed record GetInterviewRoundsSummaryQuery(
    EmployeeDepartment? EmployeeDepartment,
    AppliedPosition? AppliedPosition
    );
