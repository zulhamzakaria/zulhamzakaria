using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Application.InterviewProcesses.GetInterviewProcessesSummary;

public sealed record DTO(Guid Id,
    Guid CandidateId,
    string CandidateName,
    int CurrentSequence,
    InterviewProcessStatus InterviewProcessStatus,
    Guid? CurrentInterviewId,
    string? CurrentInterviewerName);
