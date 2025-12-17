using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Application.DTOs.Candidate;

public record CandidateReadDTO(
    Guid Id,
    string Name,
    string Email,
    string PhoneNumber,
    AppliedPosition AppliedPosition
    );
