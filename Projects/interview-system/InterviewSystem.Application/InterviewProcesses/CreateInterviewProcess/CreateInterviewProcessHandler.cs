using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace InterviewSystem.Application.InterviewProcesses.CreateInterviewProcess;

public sealed class CreateInterviewProcessHandler : IRequestHandler<CreateInterviewProcessCommand, Result<Guid>>
{
    private readonly IUnitOfWorkRepository _uow;
    private readonly IInterviewProcessRepository _interviewProcessRepository;

    public CreateInterviewProcessHandler(IInterviewProcessRepository interviewProcessRepository, IUnitOfWorkRepository uow)
    {
        _interviewProcessRepository = interviewProcessRepository;
        _uow = uow;
    }

    public async Task<Result<Guid>> Handle(CreateInterviewProcessCommand request, CancellationToken cancellationToken)
    {
        var result = InterviewProcess.Create(request.CandidateId,
           request.CandidateName,
           request.EmployeeDepartment,
           request.CurrentSequence,
           request.InterviewProcessStatus,
           request.CurrentInterviewId,
           request.CurrentInterviewName);

        if (result.IsFailure)
            return Result<Guid>.Failure(result.Errors);

        var newProcess = result.Value;

        await _interviewProcessRepository.AddAsync(newProcess!);
        await _uow.SaveChangesAsync();

        return Result<Guid>.Success(newProcess!.Id);
    }

}
