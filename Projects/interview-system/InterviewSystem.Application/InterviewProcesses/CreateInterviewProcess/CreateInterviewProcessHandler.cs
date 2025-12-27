using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Helpers;
using InterviewSystem.Domain.Interfaces.Repositories;
using InterviewSystem.Domain.Policies;
using MediatR;

namespace InterviewSystem.Application.InterviewProcesses.CreateInterviewProcess;

public sealed class CreateInterviewProcessHandler : IRequestHandler<CreateInterviewProcessCommand, Result<Guid>>
{
    private readonly IUnitOfWorkRepository _uow;
    private readonly IInterviewProcessRepository _interviewProcessRepository;
    private readonly ICandidateRepository _candidateRepository;

    public CreateInterviewProcessHandler(IInterviewProcessRepository interviewProcessRepository, IUnitOfWorkRepository uow, ICandidateRepository candidateRepository)
    {
        _interviewProcessRepository = interviewProcessRepository;
        _candidateRepository = candidateRepository;
        _uow = uow;
    }

    public async Task<Result<Guid>> Handle(CreateInterviewProcessCommand request, CancellationToken cancellationToken)
    {
        var candidate = await _candidateRepository.GetByIdAsync(request.CandidateId);
        if(candidate is null)
            return Result<Guid>.Failure(GenericErrors.NoRecordFound(nameof(Candidate), request.CandidateId));

        var department = AppliedPositionDepartmentMap.GetDepartment(candidate.AppliedPosition);
        if (department.IsFailure)
            return Result<Guid>.Failure(department.Errors);


        var result = InterviewProcess.Create(request.CandidateId, 
            candidate.Name,
            department.Value);

        if (result.IsFailure)
            return Result<Guid>.Failure(result.Errors);

        //interviewround policy
        var policy = InterviewRoundPolicyRegistry.GetPolicy(candidate.AppliedPosition);

        var newProcess = result.Value;

        await _interviewProcessRepository.AddAsync(newProcess!);
        await _uow.SaveChangesAsync();

        return Result<Guid>.Success(newProcess!.Id);
    }

}
