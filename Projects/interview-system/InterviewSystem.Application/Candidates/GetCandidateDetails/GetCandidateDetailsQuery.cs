using InterviewSystem.Domain.Common.ErrorHandling;
using MediatR;

namespace InterviewSystem.Application.Candidates.GetCandidateDetails;

public sealed record GetCandidateDetailsQuery(Guid Id) : IRequest<Result<GetCandidateDetailsDTO>>;
