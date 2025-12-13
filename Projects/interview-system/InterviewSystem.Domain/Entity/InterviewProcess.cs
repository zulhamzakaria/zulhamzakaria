using InterviewSystem.Domain.Common.Enum;

namespace InterviewSystem.Domain.Entity;

public class InterviewProcess : EntityBase
{
    public Guid Id { get; private set; }
    public Guid CandidateId { get; private set; }
    public EmployeeDepartment Department { get; private set; }
    public int CurrentSequence { get; private set; } //not to be confused with InterviewRound sequence
    public InterviewProcessStatus InterviewProcessStatus { get; private set; }
    public Guid InterviewerId { get; private set; }
    public string InterviewerName { get; private set; } = string.Empty;

    private InterviewProcess()
    {
        //EF Core needs this        
    }

    private InterviewProcess(Guid candidateId, EmployeeDepartment department, int currentSequence
        ,InterviewProcessStatus status, Guid interviewerId, string interviewerName)
    {
        CandidateId = candidateId;
        Department = department;
        CurrentSequence = currentSequence;
        InterviewProcessStatus = status;
        InterviewerId = interviewerId;
        InterviewerName = interviewerName;
    }

    public void Advance()
    {
        CurrentSequence ++;
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
