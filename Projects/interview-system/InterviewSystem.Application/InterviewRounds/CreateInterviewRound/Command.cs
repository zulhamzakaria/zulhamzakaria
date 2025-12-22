using InterviewSystem.Domain.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace InterviewSystem.Application.InterviewRounds.CreateInterviewRound;

public sealed record Command(
    [Required] EmployeeDepartment EmployeeDepartment,
    [Required] AppliedPosition AppliedPosition,
    [Required] List<EmployeePosition> EmployeePositions
    );
