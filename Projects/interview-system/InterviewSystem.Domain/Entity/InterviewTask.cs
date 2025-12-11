using InterviewSystem.Domain.Common.Enum;

namespace InterviewSystem.Domain.Entity;

public class InterviewTask : EntityBase
{
    public Guid Id { get; private set; }
    public Guid InterviewRoundId { get; private set; }
    public Guid CandidateId { get; private set; }
    public InterviewTaskStatus InterviewTaskStatus { get; private set; }
    public DateTimeOffset AssignedAt { get; private set; }
    public DateTimeOffset? CompletedAt { get; private set; }
    public CandidateEvaluation? Evaluation { get; set; }
    public bool Rejected { get; set; } = false;
    public string? TaskRejectionReason { get; set; }

    public InterviewTask()
    {
        //EF needs this  
    }

    public InterviewTask(Guid id, Guid interviewRoundId, Guid candidateId,
        DateTimeOffset assignedAt, bool rejected, string reason
        )
    {
        Id = id;
        InterviewRoundId = interviewRoundId;
        CandidateId = candidateId;
        AssignedAt = assignedAt;
        Rejected = rejected;
        TaskRejectionReason = reason;
    }

    public void MarkCompleted(bool recommendedPass, string? notes)
    {
        //TODO: Result<T>
        if (InterviewTaskStatus is InterviewTaskStatus.Completed)
            return;

        InterviewTaskStatus = InterviewTaskStatus.Completed;
        Evaluation = recommendedPass ? CandidateEvaluation.Pass(notes)
            : CandidateEvaluation.Fail(notes!);
        CompletedAt = DateTimeOffset.UtcNow;
        SetUpdated();
    }
}
