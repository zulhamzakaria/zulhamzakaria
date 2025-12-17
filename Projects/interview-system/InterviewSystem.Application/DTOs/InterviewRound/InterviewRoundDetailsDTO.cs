using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Application.DTOs.InterviewRound;

public record InterviewRoundDetailsDTO(
    Guid Id,
    EmployeeDepartment EmployeeDepartment,
    IReadOnlyList<EmployeePosition> Positions
    );
