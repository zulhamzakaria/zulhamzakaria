using InterviewSystem.Domain.Common.Enum;

namespace InterviewSystem.Domain.Entity;

public class InterviewProcess : EntityBase
{
    public Guid Id { get; private set; }
    public Guid CandidateId { get; private set; }
    public EmployeeDepartment Department { get; private set; }
    public int CurrentSequence { get; private set; }
    public 

}
