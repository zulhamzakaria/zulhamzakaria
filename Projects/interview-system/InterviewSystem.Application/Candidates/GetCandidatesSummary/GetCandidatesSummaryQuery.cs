using InterviewSystem.Domain.Common.Enums;
using InterviewSystem.Domain.Common.ErrorHandling;
using MediatR;

namespace InterviewSystem.Application.Candidates.GetCandidatesSummary;

public sealed record GetCandidatesSummaryQuery(
    AppliedPosition? AppliedPosition,
    bool? SubmittedForInterview) : IRequest<Result<IReadOnlyCollection<GetCandidatesSummaryDTO>>>;
