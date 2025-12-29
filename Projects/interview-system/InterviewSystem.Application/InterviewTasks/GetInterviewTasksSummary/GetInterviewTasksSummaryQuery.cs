using InterviewSystem.Domain.Common.Enums;
using InterviewSystem.Domain.Common.ErrorHandling;
using MediatR;

namespace InterviewSystem.Application.InterviewTasks.GetInterviewTasksSummary;

public sealed record GetInterviewTasksSummaryQuery(
        EmployeeDepartment? EmployeeDepartment,
        AppliedPosition? AppliedPosition,
        InterviewTaskStatus? InterviewTaskStatus) : IRequest<Result<IReadOnlyCollection<GetInterviewTasksSummaryDTO>>>;
