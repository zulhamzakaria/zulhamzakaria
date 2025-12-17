using InterviewSystem.Domain.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace InterviewSystem.Application.DTOs.InterviewProcess;

public record InterviewProcessCreateDTO(
    [Required] Guid CandidateId,
    [Required] EmployeeDepartment EmployeeDepartment,
    [Required] int CurrentSequence,
    [Required] InterviewProcessStatus InterviewProcessStatus,
    [Required] Guid CurrentInterviewId,
    [Required] string CurrentInterviewName
    );
