using InterviewSystem.Domain.Common.Enum;

namespace InterviewSystem.Domain.Entity;

public class InterviewTask : EntityBase
{
    public Guid Id { get; private set; }
    public Guid InterviewRoundId { get; private set; }
    public Guid CandidateId { get; private set; }
    public InterviewTaskStatus InterviewTaskStatus { get; private set; }
    public DateTimeOffset AssignedAt { get; private set; }
    public DateTimeOffset CompletedAt { get; private set; }
    public CandidateEvaluation? Evaluation { get; set; }
    public bool Rejected { get; set; }
    public string? Reason { get; set; }

    public InterviewTask()
    {
       //EF needs this  
    }

    public InterviewTask(Guid id, Guid interviewRoundId, Guid candidateId,
        InterviewTaskStatus status, DateTimeOffset assignedAt, DateTimeOffset completedAt,
        bool recommendedPass, bool rejected, string reason
        )
    {
        Id = id;
        InterviewRoundId = interviewRoundId;
        CandidateId = candidateId;
        InterviewTaskStatus = status;
        AssignedAt = assignedAt;
        CompletedAt = completedAt;
        Rejected = rejected;
        Reason = reason;
    }

    public void MarkCompleted(bool recommendedPass, string? notes)
    {
        InterviewTaskStatus = InterviewTaskStatus.Completed;
        Evaluation = recommendedPass ? CandidateEvaluation.Pass(notes)
            :CandidateEvaluation.Fail(notes!);
        CompletedAt = DateTimeOffset.UtcNow;
        SetUpdated();
    }
}
