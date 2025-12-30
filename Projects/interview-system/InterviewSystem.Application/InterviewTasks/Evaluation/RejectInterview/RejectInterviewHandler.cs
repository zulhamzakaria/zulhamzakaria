using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace InterviewSystem.Application.InterviewTasks.Evaluation.RejectInterview;

public sealed class RejectInterviewHandler : IRequestHandler<RejectInterviewCommand, Result<Guid>>
{
    private readonly IUnitOfWorkRepository _uow;
    private readonly IInterviewProcessRepository _interviewProcessRepository;
    private readonly IInterviewTaskRepository _interviewTaskRepository;

    public RejectInterviewHandler(IUnitOfWorkRepository uow, IInterviewTaskRepository interviewTaskRepository,
        IInterviewProcessRepository interviewProcessRepository)
    {
        _uow = uow;
        _interviewTaskRepository = interviewTaskRepository;
        _interviewProcessRepository = interviewProcessRepository;
    }

    public async Task<Result<Guid>> Handle(RejectInterviewCommand request, CancellationToken cancellationToken)
    {
        //task existance
        var task = await _interviewTaskRepository.GetByIdAsync(request.TaskId);
        if (task is null)
            return Result<Guid>.Failure(GenericErrors.NoRecordFound(nameof(InterviewTask), request.TaskId));

        //task ownership
        var isOwner = await _interviewTaskRepository.IsAssigneeOwner(request.TaskId);

        return Result<Guid>.Success(request.TaskId);
    }
}
