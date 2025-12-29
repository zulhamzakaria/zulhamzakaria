using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Domain.Common.ErrorHandling.Errors;

public static class InterviewTaskErrors
{
    public static Error PendingTasks()
        => new(ErrorType.Validation, "ACTIVE_PENDING_TASKS", "Cannot Deactivate Employee with Active Pending Tasks");
    public static Error BackdatedInterviewDate()
        => new(ErrorType.Validation, "INTERVIEW_DATE_BACKDATED", "Cannot set the Interview Date before current date");
    public static Error InterviewDateTooFar()
        => new(ErrorType.BusinessRule, "INTERVIEW_DATE_TOO_FAR", "Cannot set the Interview Date too far in the future");
    public static Error InterviewDateExists()
        => new(ErrorType.BusinessRule, "INTERVIEW_DATE_EXISTS", "Interview Date has been set. Please Reschedule instead");
     public static Error InterviewDateDoesntExist()
        => new(ErrorType.BusinessRule, "INTERVIEW_DATE_NOT_SET", "Interview Date has not been set. Please Schedule first");
    public static Error InvalidScheduling()
        => new(ErrorType.Validation, "INTERVIEW_DATE_INVALID", "Cannot set Interview Date on Saturday or Sunday and outside working hours");
    public static Error CompletedTask()
        => new(ErrorType.BusinessRule, "TASK_COMPLETED", "Cannot Reschedule for a completed task");
    public static Error InvalidAction(Guid assigneeId, Guid taskId)
            => new(ErrorType.Validation, "TASK_OWNER_INVALID", $"The Employee:{assigneeId}  cannot act on this Task:{taskId}");
}
