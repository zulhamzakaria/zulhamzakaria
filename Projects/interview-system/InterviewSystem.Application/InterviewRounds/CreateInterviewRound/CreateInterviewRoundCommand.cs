using InterviewSystem.Domain.Common.Enums;
using InterviewSystem.Domain.Common.ErrorHandling;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace InterviewSystem.Application.InterviewRounds.CreateInterviewRound;

public sealed record CreateInterviewRoundCommand(
    [Required] EmployeeDepartment EmployeeDepartment,
    [Required] AppliedPosition AppliedPosition,
    [Required] List<EmployeePosition> EmployeePositions
    ): IRequest<Result<Guid>> ;
