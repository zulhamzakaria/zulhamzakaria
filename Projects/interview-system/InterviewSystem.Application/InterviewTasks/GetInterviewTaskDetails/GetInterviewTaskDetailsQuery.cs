using InterviewSystem.Domain.Common.ErrorHandling;
using MediatR;

namespace InterviewSystem.Application.InterviewTasks.GetInterviewTaskDetails;

public sealed record GetInterviewTaskDetailsQuery(Guid Id) : IRequest<Result<GetInterviewTaskDetailsDTO>>;
