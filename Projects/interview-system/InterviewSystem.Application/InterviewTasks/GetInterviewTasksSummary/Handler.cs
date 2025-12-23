using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;

namespace InterviewSystem.Application.InterviewTasks.GetInterviewTasksSummary;

public sealed class Handler
{
    private readonly IInterviewTaskRepository _interviewTaskRepository;
    private readonly IInterviewTaskQueryRepository _interviewTaskQueryRepository;

    public Handler(IInterviewTaskRepository interviewTaskRepository,
        IInterviewTaskQueryRepository interviewTaskQueryRepository)
    {
        _interviewTaskRepository = interviewTaskRepository;
        _interviewTaskQueryRepository = interviewTaskQueryRepository;
    }

    public async Task<Result<IReadOnlyCollection<InterviewTasksSummaryDTO>>> HandleAsync(Query query)
    {
        var results = (await _interviewTaskQueryRepository.GetSummaryAsync())
            .Where(cq => query.EmployeeDepartment == null || cq.EmployeeDepartment == query.EmployeeDepartment)
            .Where(cq => query.AppliedPosition == null || cq.AppliedPosition == query.AppliedPosition)
            .Where(cq => query.InterviewTaskStatus == null || cq.InterviewTaskStatus == query.InterviewTaskStatus)
            .ToList();

        if (results.Any() is false)
            return Result<IReadOnlyCollection<InterviewTasksSummaryDTO>>
                .Failure(GenericErrors.NoRecordsFound(nameof(InterviewTask)));

        return Result<IReadOnlyCollection<InterviewTasksSummaryDTO>>.Success(results);
    }
}
