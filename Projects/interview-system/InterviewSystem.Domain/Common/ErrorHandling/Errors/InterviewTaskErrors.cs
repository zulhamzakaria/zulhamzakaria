using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Domain.Common.ErrorHandling.Errors;

public static class InterviewTaskErrors
{
    public static Error PendingTasks()
        => new(ErrorType.Validation, "ACTIVE_PENDING_TASKS", "Cannot Deactivate Employee with Active Pending Tasks");
}
