using InterviewSystem.Domain.Common;
using InterviewSystem.Domain.Common.Enums;
using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;

namespace InterviewSystem.Domain.Entity;

public class InterviewProcess : EntityBase
{
    public Guid Id { get; private set; }
    public Guid CandidateId { get; private set; }
    public string CandidateName { get; private set; } = string.Empty;
    public EmployeeDepartment Department { get; private set; }
    public int CurrentRoundSequence { get; private set; }
    public InterviewProcessStatus InterviewProcessStatus { get; private set; }
    public Guid? CurrentInterviewerId { get; private set; }
    public string? CurrentInterviewerName { get; private set; } = string.Empty;

    private InterviewProcess()
    {
        //EF Core needs this        
    }

    //private InterviewProcess(Guid candidateId, EmployeeDepartment department, int currentSequence
    //    , InterviewProcessStatus status, Guid interviewerId, string interviewerName)
    //{
    //    CandidateId = candidateId;
    //    Department = department;
    //    CurrentSequence = currentSequence;
    //    InterviewProcessStatus = status;
    //    CurrentInterviewerId = interviewerId;
    //    CurrentInterviewerName = interviewerName;
    //}

    public static Result<InterviewProcess> Create(Guid candidateId, 
        string candidateName, EmployeeDepartment employeeDepartment)
    {
        List<Error> errors = new();

        if (candidateId == Guid.Empty)
        {
            errors.Add(GenericErrors.Required(nameof(candidateId)));
        }
        if (string.IsNullOrWhiteSpace(candidateName))
        {
            errors.Add(GenericErrors.Required(nameof(candidateName)));
        }
        if (Enum.IsDefined(employeeDepartment) is false)
        {
            errors.Add(GenericErrors.InvalidEnumValue(employeeDepartment));
        }

        if (errors.Any())
        {
            return Result<InterviewProcess>.Failure(errors);
        }

        var process = new InterviewProcess()
        {
            Id = Guid.NewGuid(),
            CandidateId = candidateId,
            CandidateName = candidateName,
            Department = employeeDepartment,
            CurrentRoundSequence = 1,
            InterviewProcessStatus = InterviewProcessStatus.InProcess,
            CurrentInterviewerId = null,
            CurrentInterviewerName = null
        };

        return Result<InterviewProcess>.Success(process);

    }

    public Result<Unit> Advance(int nextSequence)
    {
        if (InterviewProcessStatus == InterviewProcessStatus.Completed)
            return Result<Unit>.Failure(InterviewProcessErrors.CannotAdvance());
        
        if (CurrentRoundSequence < nextSequence)
            return Result<Unit>.Failure(InterviewProcessErrors.InvalidSequence());
        
        CurrentRoundSequence = nextSequence;

        return Result<Unit>.Success(Unit.Value);
    }

    public Result<Unit> MarkCompleted()
    {
        if (InterviewProcessStatus == InterviewProcessStatus.Completed)
            return Result<Unit>.Failure(InterviewProcessErrors.InvalidStatusChange());

        InterviewProcessStatus = InterviewProcessStatus.Completed;

        return Result<Unit>.Success(Unit.Value);
    }

    public Result<Unit> MarkFailed()
    {
        if (InterviewProcessStatus == InterviewProcessStatus.Completed)
            return Result<Unit>.Failure(InterviewProcessErrors.InvalidStatusChange());

        InterviewProcessStatus = InterviewProcessStatus.Failed;

        return Result<Unit>.Success(Unit.Value);
    }


}
