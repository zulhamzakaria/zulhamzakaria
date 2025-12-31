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
    public static Error InvalidSchedulingWeekend()
        => new(ErrorType.Validation, "INTERVIEW_DATE_INVALID", "Cannot set Interview Date on Saturday/Sunday");
      public static Error InvalidSchedulingWorkingHours()
        => new(ErrorType.Validation, "INTERVIEW_DATE_INVALID", "Cannot set Interview Date outside working hours");
    public static Error CompletedTask()
        => new(ErrorType.BusinessRule, "TASK_COMPLETED", "Cannot act on a completed/rejected task");
    public static Error NotAssignedTask()
       => new(ErrorType.BusinessRule, "NOT_ASSIGNED_STATUS", "Can only act on Assigned status");
    public static Error NotAcceptedTask()
      => new(ErrorType.BusinessRule, "NOT_ACCEPTED_STATUS", "Can only act on Accepted status");
    public static Error InvalidAction(Guid assigneeId, Guid taskId)
            => new(ErrorType.Validation, "TASK_OWNER_INVALID", $"The Employee:{assigneeId}  cannot act on this Task:{taskId}");
    public static Error NoEligibleInterviewer()
            => new(ErrorType.NotFound, "NO_EMPLOYEE_RECORD", "No eligible Employee to act as Interviewer ");
    public static Error NoInterviewDate()
            => new(ErrorType.NotFound, "INTERVIEW_DATE_NOT_SET", "Please schedule the Interview first");
    public static Error InvalidTimeOffset()
        => new(ErrorType.Validation, "OFFSET_NOT_SGT", "Time offset must be in SGT (+8:00)");
    public static Error NoTaskRegistered(Guid processId, int seq)
        => new(ErrorType.Validation, "TASK_NOT_REGISTERED", $"No Task for ProcessId:{processId} and Round:{seq}");
    public static Error NoActiveTasks(Guid assigneeId)
        => new(ErrorType.Validation, "ASSIGNEE_NO_TASKS", $"No active Tasks registered under EmployeeId:{assigneeId}");
    public static Error TimeSlotTaken()
        => new(ErrorType.BusinessRule, "TIME_SLOT_TAKEN", "An active Interview Task already occupies the time slot");
    public static Error IncompleteEvaluation(Guid processId, int seq)
        => new(ErrorType.BusinessRule, "EVALUATION_NOT_COMPLETED", 
            $"Please complete all evaluations for ProcessId:{processId} and Round:{seq}");
}
