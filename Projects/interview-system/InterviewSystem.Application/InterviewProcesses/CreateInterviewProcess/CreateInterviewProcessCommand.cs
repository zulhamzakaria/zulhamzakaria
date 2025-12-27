using InterviewSystem.Domain.Common.ErrorHandling;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace InterviewSystem.Application.InterviewProcesses.CreateInterviewProcess;

public sealed record CreateInterviewProcessCommand([Required] Guid CandidateId) : IRequest<Result<Guid>>;