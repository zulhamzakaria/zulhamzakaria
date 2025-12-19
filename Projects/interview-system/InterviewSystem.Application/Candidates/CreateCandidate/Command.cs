using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Application.Candidates.CreateCandidate;

public sealed record Command(
    string Name,
    string Email,
    string PhoneNo,
    AppliedPosition AppliedPosition
    );
