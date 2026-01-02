using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;
using InterviewSystem.Domain.Entity;
using MediatR;

namespace InterviewSystem.Application.InterviewTasks.GetInterviewTasksSummary;

public sealed class GetInterviewTasksSummaryHandler :
    IRequestHandler<GetInterviewTasksSummaryQuery, Result<IReadOnlyCollection<GetInterviewTasksSummaryDTO>>>
{
    private readonly IInterviewTasksSummaryQueryRepository _interviewTaskQueryRepository;

    public GetInterviewTasksSummaryHandler(IInterviewTasksSummaryQueryRepository interviewTaskQueryRepository)
    {
        _interviewTaskQueryRepository = interviewTaskQueryRepository;
    }

    public async Task<Result<IReadOnlyCollection<GetInterviewTasksSummaryDTO>>> Handle
        (GetInterviewTasksSummaryQuery request, CancellationToken cancellationToken)
    {
        var results = (await _interviewTaskQueryRepository.GetSummaryAsync())
             .Where(cq => request.EmployeeDepartment == null || cq.EmployeeDepartment == request.EmployeeDepartment)
             .Where(cq => request.AppliedPosition == null || cq.AppliedPosition == request.AppliedPosition)
             .Where(cq => request.InterviewTaskStatus == null || cq.InterviewTaskStatus == request.InterviewTaskStatus)
             .Where(cq => request.InterviewProcessId == null || cq.InterviewProcessId == request.InterviewProcessId)
             .ToList();

        if (results.Any() is false)
            return Result<IReadOnlyCollection<GetInterviewTasksSummaryDTO>>
                .Failure(GenericErrors.NoRecordsFound(nameof(InterviewTask)));

        return Result<IReadOnlyCollection<GetInterviewTasksSummaryDTO>>.Success(results);
    }
}
