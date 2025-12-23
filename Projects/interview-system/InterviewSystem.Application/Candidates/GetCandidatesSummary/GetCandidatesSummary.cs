using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Application.Candidates.GetCandidatesSummary;

public sealed record GetCandidatesSummary(Guid Id,
    string Name,
    string Email,
    string PhoneNumber,
    AppliedPosition AppliedPosition);
