using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Application.InterviewRounds.GetInterviewRoundsSummary;

public sealed record DTO(
    Guid Id,
    EmployeeDepartment EmployeeDepartment,
    AppliedPosition AppliedPosition,
    int NumberOfRound
    );
