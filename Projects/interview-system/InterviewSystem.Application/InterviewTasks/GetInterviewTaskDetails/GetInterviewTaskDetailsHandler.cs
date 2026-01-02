using InterviewSystem.Domain.Common;
using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace InterviewSystem.Application.InterviewTasks.GetInterviewTaskDetails;

public sealed class GetInterviewTaskDetailsHandler :
    IRequestHandler<GetInterviewTaskDetailsQuery, Result<GetInterviewTaskDetailsDTO>>
{
    private readonly IInterviewTaskRepository _repo;

    public GetInterviewTaskDetailsHandler(IInterviewTaskRepository repo)
    {
        _repo = repo;
    }

    public async Task<Result<GetInterviewTaskDetailsDTO>> Handle
        (GetInterviewTaskDetailsQuery request, CancellationToken cancellationToken)
    {
        var result = await _repo.GetByIdAsync(request.Id);

        if (result == null)
            return Result<GetInterviewTaskDetailsDTO>.Failure(GenericErrors.NoRecordFound(nameof(InterviewTask), request.Id));

        var returnResult = MapToDTO(result);

        return Result<GetInterviewTaskDetailsDTO>.Success(returnResult);
    }

    private GetInterviewTaskDetailsDTO MapToDTO(InterviewTask task) =>
        new GetInterviewTaskDetailsDTO(
            Id: task.Id,
            InterviewProcessId: task.InterviewProcessId,
            task.CandidateId,
            task.CandidateName,
            task.AssigneeId,
            task.InterviewTaskStatus,
            task.AssignedAt.ToOffset(Timezones.Singapore),
            task.CompletedAt?.ToOffset(Timezones.Singapore),
            task.Evaluation,
            task.Rejected,
            task.TaskRejectionReason
            );
}
