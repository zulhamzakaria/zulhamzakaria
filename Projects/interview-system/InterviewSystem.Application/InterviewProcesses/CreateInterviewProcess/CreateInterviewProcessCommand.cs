using InterviewSystem.Domain.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace InterviewSystem.Application.InterviewProcesses.CreateInterviewProcess;

public sealed record CreateInterviewProcessCommand(
    [Required] Guid CandidateId,
    [Required] string CandidateName,
    [Required] EmployeeDepartment EmployeeDepartment,
    [Required] int CurrentSequence,
    [Required] InterviewProcessStatus InterviewProcessStatus,
    Guid CurrentInterviewId, //inserted by system?
    string CurrentInterviewName); //inserted by system?
