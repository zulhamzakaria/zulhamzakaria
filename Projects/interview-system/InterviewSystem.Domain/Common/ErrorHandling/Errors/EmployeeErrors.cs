namespace InterviewSystem.Domain.Common.ErrorHandling.Errors;

public static class EmployeeErrors
{
    public static Error InvalidEmployeeStatus()
        => new("EMPLOYEE_STATUS_INVALID", "Cannot Deactivate an Inactive Employee");
}
