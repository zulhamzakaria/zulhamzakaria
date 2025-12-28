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
    private readonly IInterviewRoundRepository _interviewRoundRepository;
    private readonly IInterviewTaskRepository _interviewTaskRepository;
    private readonly ICandidateRepository _candidateRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public CreateInterviewProcessHandler(IInterviewProcessRepository interviewProcessRepository,
        IUnitOfWorkRepository uow, ICandidateRepository candidateRepository,
        IEmployeeRepository employeeRepository, IInterviewRoundRepository interviewRoundRepository, 
        IInterviewTaskRepository interviewTaskRepository)
    {
        _interviewProcessRepository = interviewProcessRepository;
        _interviewRoundRepository = interviewRoundRepository;
        _interviewTaskRepository = interviewTaskRepository;
        _candidateRepository = candidateRepository;
        _employeeRepository = employeeRepository;
        _uow = uow;
    }

    public async Task<Result<Guid>> Handle(CreateInterviewProcessCommand request, CancellationToken cancellationToken)
    {
        var candidate = await _candidateRepository.GetByIdAsync(request.CandidateId);
        if (candidate is null)
            return Result<Guid>.Failure(GenericErrors.NoRecordFound(nameof(Candidate), request.CandidateId));

        var department = AppliedPositionDepartmentMap.GetDepartment(candidate.AppliedPosition);
        if (department.IsFailure)
            return Result<Guid>.Failure(department.Errors);

        var process = await _interviewProcessRepository.GetByCandidateIdAsync(request.CandidateId);
        if (process is not null)
            return Result<Guid>.Failure(InterviewProcessErrors.ProcessExists(request.CandidateId));

        var result = InterviewProcess.Create(request.CandidateId,
            candidate.Name,
            department.Value);

        if (result.IsFailure)
            return Result<Guid>.Failure(result.Errors);

        //InterviewRound
        var policy = InterviewRoundPolicyRegistry.GetPolicy(candidate.AppliedPosition);
        if (policy.IsFailure)
            return Result<Guid>.Failure(policy.Errors);

        var roundPolicy = policy.Value;
        var interviewRound = InterviewRound.Create(
            roundPolicy.AppliedDepartment,
            candidate.AppliedPosition,
            roundPolicy.Steps);

        if (interviewRound.IsFailure)
            return Result<Guid>.Failure(interviewRound.Errors);

        //InterviewTask
        var employeePosition = roundPolicy.Steps.FirstOrDefault();

        if (employeePosition is null)
            return Result<Guid>.Failure(InterviewRoundErrors.UndefinedInitiator());

        var employees = await _employeeRepository.GetEmployeesByPositionAsync
            (employeePosition.AllowedPositions.FirstOrDefault());

        if (employees.Any() is false)
            return Result<Guid>.Failure(GenericErrors.NoRecordsFound(nameof(employeePosition)));

        var currentInterviewer = new
        {
            Id = employees.FirstOrDefault()!.Id,
            Name = employees.FirstOrDefault()!.Name ?? string.Empty
        };

        var initialTask = InterviewTask.Create(
            interviewRound.Value!.Id,
            result.Value!.Id,
            request.CandidateId,
            candidate.Name,
            currentInterviewer.Id,
            currentInterviewer.Name);

        if (initialTask.IsFailure)
            return Result<Guid>.Failure(initialTask.Errors);

        var newProcess = result.Value;
        var newInterviewRound = interviewRound.Value;
        var newTask = initialTask.Value!;

        newProcess.UpdateCurrentInterviewer(currentInterviewer.Id, currentInterviewer.Name);

        await _interviewProcessRepository.AddAsync(newProcess);
        await _interviewRoundRepository.AddAsync(newInterviewRound);
        await _interviewTaskRepository.AddAsync(newTask);

        await _uow.SaveChangesAsync();

        return Result<Guid>.Success(newProcess!.Id);
    }

}
