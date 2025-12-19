using InterviewSystem.Domain.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace InterviewSystem.Application.Candidates.CreateCandidate;

public sealed record Command(
    [Required, StringLength(100, MinimumLength = 1)] string Name,
    [Required, StringLength(100, MinimumLength = 1), EmailAddress] string Email,
    [Required, StringLength(20, MinimumLength = 1)] string PhoneNumber,
    [Required] AppliedPosition AppliedPosition
    );
