using InterviewSystem.Domain.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace InterviewSystem.Application.InterviewTasks.CreateInterviewTask;

public sealed record CreateInterviewTaskCommand(
    [Required] Guid InterviewRoundId,
    [Required] Guid CandidateId,
    [Required] string CandidateName,
    [Required] InterviewTaskStatus InterviewTaskStatus,
    [Required] DateTimeOffset AssignedAt);
