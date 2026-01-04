using InterviewSystem.Domain.Common;
using InterviewSystem.Domain.Common.Enums;
using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;
using InterviewSystem.Domain.Rules;

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
    public Guid? RejectionOriginatorId { get; private set; }

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

    public Result<Unit> MarkCompleted(bool recommendedPass, string? notes)
    {
        var errors = new List<Error>();

        //valid on Accepted task
        if (InterviewTaskStatus is not InterviewTaskStatus.Accepted)
            errors.Add(InterviewTaskErrors.NotAcceptedTask());

        if (recommendedPass is false && string.IsNullOrWhiteSpace(notes))
            errors.Add(GenericErrors.Required(nameof(notes)));

        if (InterviewDate is null)
            errors.Add(InterviewTaskErrors.NoInterviewDate());

        if (recommendedPass == false && string.IsNullOrWhiteSpace(notes))
            errors.Add(GenericErrors.Required(nameof(notes)));

        if (errors.Any())
            return Result<Unit>.Failure(errors);

        InterviewTaskStatus = recommendedPass 
            ? InterviewTaskStatus.Passed 
            : InterviewTaskStatus.Failed;

        Evaluation = recommendedPass ? CandidateEvaluation.Pass(notes)
            : CandidateEvaluation.Fail(notes!);
        CompletedAt = DateTimeOffset.UtcNow;

        SetUpdated();

        return Result<Unit>.Success(new Unit());
    }

    public Result<Unit> RejectTask(string rejectionReason)
    {

        List<Error> errors = new();

        if(InterviewTaskRules.ActiveStatuses.Contains(InterviewTaskStatus) is false)
            errors.Add(InterviewTaskErrors.NotActiveTask());  

        if(string.IsNullOrWhiteSpace(rejectionReason))
            errors.Add(GenericErrors.Required(nameof(rejectionReason)));

        if (string.IsNullOrWhiteSpace(rejectionReason) is false 
            && rejectionReason.Length > RejectionReasonMaxLength)
            errors.Add(GenericErrors.InvalidLength(nameof(rejectionReason),
                RejectionReasonMinLength, RejectionReasonMaxLength));

        InterviewTaskStatus = InterviewTaskStatus.Rejected;
        Rejected = true;
        TaskRejectionReason = rejectionReason;

        return Result<Unit>.Success(new Unit());
    }

    public void SetRejectionOriginator(Guid assigneeId)
    {
        if (RejectionOriginatorId is null)
            RejectionOriginatorId = assigneeId;
    }
    public Result<Unit> Scheduling(DateTimeOffset interviewDate)
    {
        var errors = new List<Error>();

        var result = ValidateDate(interviewDate);

        if (result.IsFailure)
            errors.AddRange(result.Errors);

        if (InterviewDate is not null)
            errors.Add(InterviewTaskErrors.InterviewDateExists());

        //valid on Assigned Task
        if (InterviewTaskStatus != InterviewTaskStatus.Assigned)
            errors.Add(InterviewTaskErrors.NotAssignedTask());

        if (errors.Any())
            return Result<Unit>.Failure(errors);

        InterviewDate = interviewDate.ToUniversalTime();
        InterviewTaskStatus = InterviewTaskStatus.Accepted;

        return Result<Unit>.Success(new Unit());
    }

    public Result<Unit> Rescheduling(DateTimeOffset interviewDate)
    {
        var errors = new List<Error>();

        var result = ValidateDate(interviewDate);

        if (result.IsFailure)
            errors.AddRange(result.Errors);

        //valid on accepted task
        if (InterviewTaskStatus != InterviewTaskStatus.Accepted)
            errors.Add(InterviewTaskErrors.NotAcceptedTask());

        if (InterviewDate is null)
            errors.Add(InterviewTaskErrors.InterviewDateDoesntExist());

        if (errors.Any())
            return Result<Unit>.Failure(errors);

        InterviewDate = interviewDate.ToUniversalTime();
        InterviewTaskStatus = InterviewTaskStatus.Accepted;

        return Result<Unit>.Success(new Unit());
    }

    public void Reassignment()
    {
        InterviewTaskStatus = InterviewTaskStatus.Assigned;
    }

    private Result<DateTimeOffset> ValidateDate(DateTimeOffset interviewDate)
    {
        var errors = new List<Error>();

        if (interviewDate.Offset != TimeSpan.FromHours(8))
            errors.Add(InterviewTaskErrors.InvalidTimeOffset());

        if (interviewDate < DateTimeOffset.UtcNow)
            errors.Add(InterviewTaskErrors.BackdatedInterviewDate());

        if (interviewDate > DateTimeOffset.UtcNow.AddYears(1))
            errors.Add(InterviewTaskErrors.InterviewDateTooFar());

        if (interviewDate.Hour < 9 || interviewDate.Hour > 17)
            errors.Add(InterviewTaskErrors.InvalidSchedulingWorkingHours());

        if (interviewDate.DayOfWeek == DayOfWeek.Saturday
            || interviewDate.DayOfWeek == DayOfWeek.Sunday)
            errors.Add(InterviewTaskErrors.InvalidSchedulingWeekend());

        if (errors.Any())
            return Result<DateTimeOffset>.Failure(errors);

        return Result<DateTimeOffset>.Success(interviewDate);
    }

}
