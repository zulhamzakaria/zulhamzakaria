using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace InterviewSystem.Application.InterviewTasks.Scheduling.SetInterviewDate;

public sealed class SetInterviewDateHandler : IRequestHandler<SetInterviewDateCommand, Result<DateTimeOffset>>
{
    private readonly IInterviewTaskRepository _interviewTaskRepository;
    private readonly IUnitOfWorkRepository _uow;
    public SetInterviewDateHandler(IUnitOfWorkRepository uow, IInterviewTaskRepository interviewTaskRepository)
    {
        _uow = uow;
        _interviewTaskRepository = interviewTaskRepository;
    }
    public Task<Result<DateTimeOffset>> Handle(SetInterviewDateCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
