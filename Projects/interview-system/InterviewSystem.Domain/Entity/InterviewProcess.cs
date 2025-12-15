using InterviewSystem.Domain.Common.Enums;
using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;

namespace InterviewSystem.Domain.Entity;

public class InterviewProcess : EntityBase
{
    public Guid Id { get; private set; }
    public Guid CandidateId { get; private set; }
    public EmployeeDepartment Department { get; private set; }
    public int CurrentSequence { get; private set; } //not to be confused with InterviewRound sequence
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

    public static Result<InterviewProcess> Create(Guid candidateId, EmployeeDepartment employeeDepartment,
        int currentSequence, InterviewProcessStatus interviewProcessStatus,
        Guid currentInterviewerId, string currentInterviewerName)
    {
        List<Error> errors = new();

        if (candidateId == Guid.Empty)
        {
            errors.Add(GenericErrors.Required(nameof(candidateId)));
        }
        if (Enum.IsDefined(employeeDepartment) is false)
        {
            errors.Add(GenericErrors.InvalidEnumValue(employeeDepartment));
        }
        if (currentSequence <= 0)
        {
            errors.Add(GenericErrors.InvalidIntValue(nameof(currentSequence)));
        }
        if (Enum.IsDefined(interviewProcessStatus) is false)
        {
            errors.Add(GenericErrors.InvalidEnumValue(interviewProcessStatus));
        }
        if (currentInterviewerId == Guid.Empty)
        {
            errors.Add(GenericErrors.Required(nameof(currentInterviewerId)));
        }
        if (string.IsNullOrWhiteSpace(currentInterviewerName))
        {
            errors.Add(GenericErrors.Required(nameof(currentInterviewerName)));
        }

        if (errors.Any())
        {
            return Result<InterviewProcess>.Failure(errors);
        }

        var process = new InterviewProcess()
        {
            Id = Guid.NewGuid(),
            CandidateId = candidateId,
            Department = employeeDepartment,
            CurrentSequence = currentSequence,
            InterviewProcessStatus = interviewProcessStatus,
            CurrentInterviewerId = currentInterviewerId,
            CurrentInterviewerName = currentInterviewerName
        };

        return Result<InterviewProcess>.Success(process);

    }

    public void Advance()
    {
        CurrentSequence++;
    }

    public void MarkFailed()
    {
        InterviewProcessStatus = InterviewProcessStatus.Failed;
    }

    public void MarkCompleted()
    {
        InterviewProcessStatus = InterviewProcessStatus.Completed;
    }
}
