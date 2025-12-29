using InterviewSystem.Domain.Common;
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
    public Guid CandidateId { get; private set; }
    public string CandidateName { get; private set; }
    public Guid InterviewProcessId { get; private set; }
    public int RoundSequence { get; private set; }
    public InterviewTaskStatus InterviewTaskStatus { get; private set; }
    public Guid AssigneeId { get; private set; }
    public string? AssigneeName { get; private set; }
    public DateTimeOffset AssignedAt { get; private set; }
    public DateTimeOffset? CompletedAt { get; private set; }
    public DateTimeOffset? InterviewDate { get; set; }
    public CandidateEvaluation? Evaluation { get; set; }
    public bool Rejected { get; set; } = false;
    public string? TaskRejectionReason { get; set; }

    private InterviewTask()
    {
        //EF needs this  
    }

    public static Result<InterviewTask> Create(Guid interviewRoundId, Guid interviewProcessId, int roundSequence,
        Guid candidateId, string candidateName,
        Guid assigneeId, string assigneeName)
    {
        List<Error> errors = new();

        if (interviewProcessId == Guid.Empty)
            errors.Add(GenericErrors.Required(nameof(interviewProcessId)));
        if (interviewRoundId == Guid.Empty)
            errors.Add(GenericErrors.Required(nameof(interviewRoundId)));
        if (candidateId == Guid.Empty)
            errors.Add(GenericErrors.Required(nameof(candidateId)));
        //if(rejected == true && string.IsNullOrWhiteSpace(rejectionReason))
        //    errors.Add(GenericErrors.Required(nameof(rejectionReason)));
        //if (string.IsNullOrWhiteSpace(rejectionReason) is false && rejectionReason.Length > RejectionReasonMaxLength)
        //    errors.Add(GenericErrors.InvalidLength(nameof(rejectionReason), RejectionReasonMinLength, RejectionReasonMaxLength));

        if (errors.Any())
            return Result<InterviewTask>.Failure(errors);

        InterviewTask task = new()
        {
            Id = Guid.NewGuid(),
            InterviewRoundId = interviewRoundId,
            InterviewProcessId = interviewProcessId,
            CandidateId = candidateId,
            CandidateName = candidateName,
            RoundSequence = roundSequence,
            AssigneeId = assigneeId,
            AssigneeName = assigneeName,
            InterviewTaskStatus = InterviewTaskStatus.Assigned,
            AssignedAt = DateTimeOffset.UtcNow,
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

    public Result<Unit> Scheduling(DateTimeOffset interviewDate)
    {
        var errors = new List<Error>();

        var result = ValidateDate(interviewDate);

        if (result.IsFailure)
            errors.AddRange(result.Errors);

        if (InterviewDate is not null)
            errors.Add(InterviewTaskErrors.InterviewDateExists());

        if (errors.Any())
            return Result<Unit>.Failure(errors);

        InterviewDate = interviewDate;

        return Result<Unit>.Success(new Unit());
    }

    public Result<Unit> Rescheduling(DateTimeOffset interviewDate)
    {
        var errors = new List<Error>();

        var result = ValidateDate(interviewDate);

        if (result.IsFailure)
            errors.AddRange(result.Errors);

        if (InterviewTaskStatus == InterviewTaskStatus.Completed)
            errors.Add(InterviewTaskErrors.CompletedTask());

        if (InterviewDate is null)
            errors.Add(InterviewTaskErrors.InterviewDateDoesntExist());

        if(errors.Any())
            return Result<Unit>.Failure(errors); 

        InterviewDate = interviewDate;

        return Result<Unit>.Success(new Unit());
    }

    private Result<DateTimeOffset> ValidateDate(DateTimeOffset interviewDate)
    {
        var errors = new List<Error>();

        if (interviewDate < DateTimeOffset.UtcNow)
            errors.Add(InterviewTaskErrors.BackdatedInterviewDate());
        if (interviewDate > DateTimeOffset.UtcNow.AddYears(1))
            errors.Add(InterviewTaskErrors.InterviewDateTooFar());
        if (interviewDate.Hour < 9 || interviewDate.Hour > 17)
            errors.Add(InterviewTaskErrors.InvalidScheduling());
        if (interviewDate.DayOfWeek == DayOfWeek.Saturday
            || interviewDate.DayOfWeek == DayOfWeek.Sunday)
            errors.Add(InterviewTaskErrors.InvalidScheduling());

        if (errors.Any())
            return Result<DateTimeOffset>.Failure(errors);

        return Result<DateTimeOffset>.Success(interviewDate);
    }
}
