using InterviewSystem.Domain.Common.Enums;
using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;

namespace InterviewSystem.Domain.Entity;

public class InterviewTask : EntityBase
{
    private const int RejectionReasonMinLength = 1;
    private const int RejectionReasonMaxLength = 500;
    public Guid Id { get; private set; }
    public Guid InterviewRoundId { get; private set; }
    //public InterviewRound? InterviewRound { get; private set; }
    public Guid CandidateId { get; private set; }
    public string CandidateName { get; private set; }
    //public Candidate? Candidate { get; private set; }
    public InterviewTaskStatus InterviewTaskStatus { get; private set; }
    public DateTimeOffset AssignedAt { get; private set; }
    public DateTimeOffset? CompletedAt { get; private set; }
    public CandidateEvaluation? Evaluation { get; set; }
    public bool Rejected { get; set; } = false;
    public string? TaskRejectionReason { get; set; }

    private InterviewTask()
    {
        //EF needs this  
    }

    //private InterviewTask(Guid id, Guid interviewRoundId, Guid candidateId,
    //    DateTimeOffset assignedAt, bool rejected, string reason
    //    )
    //{
    //    Id = id;
    //    InterviewRoundId = interviewRoundId;
    //    CandidateId = candidateId;
    //    AssignedAt = assignedAt;
    //    Rejected = rejected;
    //    TaskRejectionReason = reason;
    //}

    public static Result<InterviewTask> Create(Guid interviewRoundId, Guid candidateId, string candidateName)
    {
        List<Error> errors = new();

        if (interviewRoundId == Guid.Empty)
            errors.Add(GenericErrors.Required(nameof(interviewRoundId)));
        if (candidateId == Guid.Empty)
            errors.Add(GenericErrors.Required(nameof(candidateId)));
        //if(rejected == true && string.IsNullOrWhiteSpace(rejectionReason))
        //    errors.Add(GenericErrors.Required(nameof(rejectionReason)));
        //if (string.IsNullOrWhiteSpace(rejectionReason) is false && rejectionReason.Length > RejectionReasonMaxLength)
        //    errors.Add(GenericErrors.InvalidLength(nameof(rejectionReason), RejectionReasonMinLength, RejectionReasonMaxLength));

        if(errors.Any())
            return Result<InterviewTask>.Failure(errors);

        InterviewTask task = new()
        {
            Id = Guid.NewGuid(),
            InterviewRoundId = interviewRoundId,
            CandidateId = candidateId,
            CandidateName = candidateName,
            InterviewTaskStatus = InterviewTaskStatus.Pending,
            AssignedAt = DateTimeOffset.Now,
            Rejected = false,
            TaskRejectionReason = null
        };
        return Result<InterviewTask>.Success(task);
    }

    public void MarkCompleted(bool recommendedPass, string? notes)
    {
        //TODO: Result<T>
        if (InterviewTaskStatus is InterviewTaskStatus.Completed)
            throw new Exception("invalid move");

        InterviewTaskStatus = InterviewTaskStatus.Completed;
        Evaluation = recommendedPass ? CandidateEvaluation.Pass(notes)
            : CandidateEvaluation.Fail(notes!);
        CompletedAt = DateTimeOffset.UtcNow;
        SetUpdated();
    }
}
