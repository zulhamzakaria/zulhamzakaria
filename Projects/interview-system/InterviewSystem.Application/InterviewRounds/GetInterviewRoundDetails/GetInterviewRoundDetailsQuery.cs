using InterviewSystem.Domain.Common.ErrorHandling;
using MediatR;

namespace InterviewSystem.Application.InterviewRounds.GetInterviewRoundDetails;

public sealed record GetInterviewRoundDetailsQuery(Guid Id)
    :IRequest<Result<GetInterviewRoundDetailsDTO>>;
