using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace InterviewSystem.Application.InterviewRounds.CreateInterviewRound;

public sealed class CreateInterviewRoundHandler : IRequestHandler<CreateInterviewRoundCommand, Result<Guid>>
{
    private readonly IInterviewRoundRepository _interviewRoundRepository;
    private readonly IUnitOfWorkRepository _uow;
    public CreateInterviewRoundHandler(IInterviewRoundRepository interviewRoundRepository, IUnitOfWorkRepository uow)
    {
        _interviewRoundRepository = interviewRoundRepository;
        _uow = uow;
    }

    public async Task<Result<Guid>> Handle(CreateInterviewRoundCommand request, CancellationToken cancellationToken)
    {
        var result = InterviewRound.Create(
            request.EmployeeDepartment,
            request.AppliedPosition,
            request.EmployeePositions);

        if (result.IsFailure)
            return Result<Guid>.Failure(result.Errors);

        var newInterviewRound = result.Value!;

        await _interviewRoundRepository.AddAsync(newInterviewRound);
        await _uow.SaveChangesAsync();

        return Result<Guid>.Success(newInterviewRound.Id);
    }

}
