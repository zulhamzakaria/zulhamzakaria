using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;

namespace InterviewSystem.Application.InterviewTasks.GetInterviewTaskDetails;

public sealed class Handler
{
    private readonly IInterviewTaskRepository _repo;

    public Handler(IInterviewTaskRepository repo)
    {
        _repo = repo;
    }

    public async Task<Result<DTO>> HandleAsync(Query query)
    {
        var result = await _repo.GetByIdAsync(query.Id);

        if (result == null)
            return Result<DTO>.Failure(GenericErrors.NoRecordFound(nameof(InterviewTask), query.Id));

        var returnResult = MapToDTO(result);

        return Result<DTO>.Success(returnResult);

    }

    private DTO MapToDTO(InterviewTask task) =>
        new DTO(
            task.Id,
            task.InterviewRoundId,
            task.CandidateId,
            task.CandidateName,
            task.InterviewTaskStatus,
            task.AssignedAt,
            task.CompletedAt,
            task.Evaluation,
            task.Rejected,
            task.TaskRejectionReason
            );
}
