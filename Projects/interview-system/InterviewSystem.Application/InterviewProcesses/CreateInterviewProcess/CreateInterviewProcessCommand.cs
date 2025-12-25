using InterviewSystem.Domain.Common.Enums;
using InterviewSystem.Domain.Common.ErrorHandling;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace InterviewSystem.Application.InterviewProcesses.CreateInterviewProcess;

public sealed record CreateInterviewProcessCommand(
    [Required] Guid CandidateId,
    [Required] string CandidateName,
    [Required] EmployeeDepartment EmployeeDepartment,
    [Required] int CurrentSequence,
    [Required] InterviewProcessStatus InterviewProcessStatus,
    Guid CurrentInterviewId, //inserted by system?
    string CurrentInterviewName) : IRequest<Result<Guid>>; //inserted by system?
