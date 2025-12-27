using InterviewSystem.Domain.Common.Enums;
using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;

namespace InterviewSystem.Domain.Helpers;

public static class AppliedPositionDepartmentMap
{
    private static readonly Dictionary<AppliedPosition, EmployeeDepartment> _map
        = new()
        {
            {AppliedPosition.Clerk, EmployeeDepartment.HR },
            {AppliedPosition.TeaLady, EmployeeDepartment.HR },
            {AppliedPosition.JuniorEngineer, EmployeeDepartment.Engineering },
            {AppliedPosition.SeniorEngineer, EmployeeDepartment.Engineering },
            {AppliedPosition.TechnicalLead, EmployeeDepartment.Engineering },
        };

    public static Result<EmployeeDepartment> GetDepartment(AppliedPosition appliedPosition)
    {
        if(_map.TryGetValue(appliedPosition, out var department) is false)
            return Result<EmployeeDepartment>.Failure(InterviewProcessErrors.UndefinedDepartment(appliedPosition));

        return Result<EmployeeDepartment>.Success(department);
    }
}
