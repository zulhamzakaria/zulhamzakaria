using InterviewSystem.Domain.Common.Enums;
using InterviewSystem.Domain.Common.ErrorHandling;
using MediatR;

namespace InterviewSystem.Application.InterviewProcesses.GetInterviewProcessesSummary;

public sealed record GetInterviewProcessesSummaryQuery(
    EmployeeDepartment? EmployeeDepartment,
    InterviewProcessStatus? InterviewProcessStatus,
    Guid? InterviewerId,
    Guid? CandidateId): IRequest<Result<IReadOnlyCollection<GetInterviewProcessesSummaryDTO>>> ;
