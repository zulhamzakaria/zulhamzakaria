using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace InterviewSystem.Application.InterviewTasks.Scheduling.RejectTask;

public sealed class RejectTaskHandler : IRequestHandler<RejectTaskCommand, Result<Guid>>
{
    private readonly IInterviewTaskRepository _interviewTaskRepository;
    private readonly IUnitOfWorkRepository _uow;
    public RejectTaskHandler(IInterviewTaskRepository interviewTaskRepository, IUnitOfWorkRepository uow)
    {
        _interviewTaskRepository = interviewTaskRepository;
        _uow = uow;
    }
    public async Task<Result<Guid>> Handle(RejectTaskCommand request, CancellationToken cancellationToken)
    {
        //task not valid
        //assignee task ownership
        //cannot reject if Task not active
        //cannot reject if theres not enough interviewee

        return Result<Guid>.Success(request.TaskId);
    }
}
