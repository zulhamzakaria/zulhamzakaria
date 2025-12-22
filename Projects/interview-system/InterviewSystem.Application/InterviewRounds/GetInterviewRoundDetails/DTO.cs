using InterviewSystem.Domain.Common.Enums;
using InterviewSystem.Domain.Entity;

namespace InterviewSystem.Application.InterviewRounds.GetInterviewRoundDetails;

public sealed record DTO(
    Guid Id,
    EmployeeDepartment EmployeeDepartment,
    int NumberOfRounds,
    AppliedPosition AppliedPosition,
    IReadOnlyList<InterviewRoundItem> InterviewRoundItems
    );
