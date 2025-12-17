using InterviewSystem.Domain.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace InterviewSystem.Application.DTOs.InterviewRound;

public record InterviewRoundCreateDTO(
    [Required] EmployeeDepartment EmployeeDepartment,
    [Required] IReadOnlyList<EmployeePosition> Positions
    );
