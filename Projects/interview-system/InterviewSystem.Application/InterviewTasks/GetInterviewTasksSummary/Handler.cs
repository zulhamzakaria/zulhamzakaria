using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Interfaces.Repositories;

namespace InterviewSystem.Application.InterviewTasks.GetInterviewTasksSummary;

public sealed class Handler
{
    private readonly IInterviewTaskRepository _interviewTaskRepository;

    private readonly IInterviewRoundRepository _interviewRoundRepository;

    public Handler(IInterviewRoundRepository interviewRoundRepository,
        IInterviewTaskRepository interviewTaskRepository)
    {
        _interviewRoundRepository = interviewRoundRepository;
        _interviewTaskRepository = interviewTaskRepository;
    }

    public async Task<Result<IReadOnlyCollection<DTO>>> HandleAsync(Query query)
    {
        throw new Exception();
    }
}
