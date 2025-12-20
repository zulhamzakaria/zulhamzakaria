using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Application.InterviewProcesses.GetInterviewProcessesSummary;

public sealed record DTO(Guid Id,
    string CandidateName,
    string CurrentSequence,
    InterviewProcessStatus InterviewProcessStatus,
    string CurrentInterviewerName);
