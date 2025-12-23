using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Domain.Common.ErrorHandling.Errors;

public static class EmployeeErrors
{
    public static Error InvalidEmployeeStatus()
        => new(ErrorType.BusinessRule, "EMPLOYEE_STATUS_INVALID", "Cannot Deactivate an Inactive Employee");
}
