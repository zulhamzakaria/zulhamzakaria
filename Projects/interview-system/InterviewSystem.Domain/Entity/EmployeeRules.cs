using InterviewSystem.Domain.Common.Enums;
using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;

namespace InterviewSystem.Domain.Entity;

public static class EmployeeRules
{
    private static readonly Dictionary<EmployeeDepartment, HashSet<EmployeePosition>> AllowedPositions =
        new()
        {
            [EmployeeDepartment.HR] = new() { EmployeePosition.Clerk, EmployeePosition.HiringManager },
            [EmployeeDepartment.Engineering] = new() { EmployeePosition.SeniorEngineer, EmployeePosition.TechLead },
        };
    public static Result<bool> IsPositionValidForDepartment(EmployeeDepartment department, EmployeePosition position)
    {
        if (AllowedPositions.TryGetValue(department, out var positions) is false)
            return Result<bool>.Failure(EmployeeErrors.InvalidDepartment(department));
            
        return Result<bool>.Success(positions.Contains(position));
    }

    public static readonly HashSet<EmployeePosition> CanCoordinate = new() { EmployeePosition.Clerk };
    public static readonly HashSet<EmployeePosition> CanInterview = new() { EmployeePosition.HiringManager, 
        EmployeePosition.SeniorEngineer,
        EmployeePosition.TechLead, 
        EmployeePosition.Manager, 
        EmployeePosition.HOD};
}
