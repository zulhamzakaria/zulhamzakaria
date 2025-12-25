using InterviewSystem.Domain.Common.ErrorHandling;
using MediatR;

namespace InterviewSystem.Application.InterviewProcesses.GetInterviewProcessDetails;

public sealed record GetInterviewProcessDetailsQuery(Guid Id): IRequest<Result<GetInterviewProcessDetailsDTO>>;
