using InterviewSystem.Domain.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace InterviewSystem.Application.DTOs.InterviewTask;

public record InterviewTaskCreateDTO(
    [Required] Guid InterviewRoundId,
    [Required] Guid CandidateId,
    [Required] InterviewTaskStatus InterviewTaskStatus
    );
