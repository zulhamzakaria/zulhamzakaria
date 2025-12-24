using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;

namespace InterviewSystem.Application.InterviewTasks.GetInterviewTaskDetails;

public sealed class GetInterviewTaskDetailsHandler
{
    private readonly IInterviewTaskRepository _repo;

    public GetInterviewTaskDetailsHandler(IInterviewTaskRepository repo)
    {
        _repo = repo;
    }

    public async Task<Result<GetInterviewTaskDetailsDTO>> HandleAsync(GetInterviewTaskDetailsQuery query)
    {
        var result = await _repo.GetByIdAsync(query.Id);

        if (result == null)
            return Result<GetInterviewTaskDetailsDTO>.Failure(GenericErrors.NoRecordFound(nameof(InterviewTask), query.Id));

        var returnResult = MapToDTO(result);

        return Result<GetInterviewTaskDetailsDTO>.Success(returnResult);

    }

    private GetInterviewTaskDetailsDTO MapToDTO(InterviewTask task) =>
        new GetInterviewTaskDetailsDTO(
            task.Id,
            task.InterviewProcessId,
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
