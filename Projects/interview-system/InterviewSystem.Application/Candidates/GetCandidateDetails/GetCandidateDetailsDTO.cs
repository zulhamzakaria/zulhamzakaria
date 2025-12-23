using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Application.Candidates.GetCandidateDetails;

public sealed record GetCandidateDetailsDTO(Guid Id,
    string Name,
    string Email,
    string PhoneNumber,
    AppliedPosition AppliedPosition
    );
