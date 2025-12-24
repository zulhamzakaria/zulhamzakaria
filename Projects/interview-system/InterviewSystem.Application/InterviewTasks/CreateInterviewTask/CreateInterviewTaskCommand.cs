using InterviewSystem.Domain.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace InterviewSystem.Application.InterviewTasks.CreateInterviewTask;

public sealed record CreateInterviewTaskCommand(
    [Required] Guid InterviewRoundId,
    [Required] Guid InterviewProcessId,
    [Required] Guid CandidateId,
    [Required] string CandidateName,
    [Required] Guid AssigneeId,
    [Required] string AssigneeName,
    [Required] InterviewTaskStatus InterviewTaskStatus,
    [Required] DateTimeOffset AssignedAt);
