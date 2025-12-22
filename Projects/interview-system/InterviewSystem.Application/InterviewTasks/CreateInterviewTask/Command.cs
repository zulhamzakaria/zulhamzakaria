using InterviewSystem.Domain.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace InterviewSystem.Application.InterviewTasks.CreateInterviewTask;

public sealed record Command(
    [Required] Guid InterviewRoundId,
    [Required] Guid CandidateId,
    [Required] InterviewTaskStatus InterviewTaskStatus,
    [Required] DateTimeOffset AssignedAt);
