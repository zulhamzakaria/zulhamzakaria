using InterviewSystem.Domain.Common.Enums;
using InterviewSystem.Domain.Common.ErrorHandling;
using MediatR;

namespace InterviewSystem.Application.InterviewTasks.GetInterviewTasksSummary;

public sealed record GetInterviewTasksSummaryQuery(
        EmployeeDepartment? EmployeeDepartment,
        AppliedPosition? AppliedPosition,
        InterviewTaskStatus? InterviewTaskStatus,
        bool? Rejected,
        string? TaskRejectionReason
        ) : IRequest<Result<IReadOnlyCollection<GetInterviewTasksSummaryDTO>>>;
