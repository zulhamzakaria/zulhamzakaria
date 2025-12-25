using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Domain.Common.ErrorHandling.Errors;

public static class EmployeeErrors
{
    public static Error InvalidEmployeeStatus()
        => new(ErrorType.BusinessRule, "EMPLOYEE_STATUS_INVALID", 
            "Cannot Deactivate an Inactive Employee");

    public static Error InvalidEmployeeForDepartment( EmployeeDepartment employeeDepartment, EmployeePosition employeePosition)
        => new(ErrorType.BusinessRule, "EMPLOYEE_DEPARTMENT_MISMATCH", 
            $"{employeePosition.ToString()} cannot be registered under {employeeDepartment.ToString()}");

    public static Error InvalidEmployeeAction()
        => new(ErrorType.BusinessRule, "EMPLOYEE_ACTION_INVALID", 
            "The Employee does not have the privilege to perform this Operation");

    public static Error InvalidDepartment(EmployeeDepartment employeeDepartment)
        => new(ErrorType.BusinessRule, "DEPARTMENT_UNDEFINED",
            $"There is no definition for {employeeDepartment.ToString()} and allowed employees combination");
 }
