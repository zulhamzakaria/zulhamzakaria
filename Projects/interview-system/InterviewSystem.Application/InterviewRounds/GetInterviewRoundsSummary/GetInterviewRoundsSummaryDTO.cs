using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Application.InterviewRounds.GetInterviewRoundsSummary;

public sealed record GetInterviewRoundsSummaryDTO(
    Guid Id,
    EmployeeDepartment EmployeeDepartment,
    AppliedPosition AppliedPosition,
    int NumberOfRound
    );
