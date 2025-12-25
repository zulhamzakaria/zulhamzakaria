using InterviewSystem.Domain.Common.Enums;
using InterviewSystem.Domain.Common.ErrorHandling;
using MediatR;

namespace InterviewSystem.Application.InterviewRounds.GetInterviewRoundsSummary;

public sealed record GetInterviewRoundsSummaryQuery(
    EmployeeDepartment? EmployeeDepartment,
    AppliedPosition? AppliedPosition
    ) : IRequest<Result<IReadOnlyCollection<GetInterviewRoundsSummaryDTO>>> ;
