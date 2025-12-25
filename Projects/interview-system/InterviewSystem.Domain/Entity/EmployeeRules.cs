using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Domain.Entity;

public static class EmployeeRules
{
    private static readonly Dictionary<EmployeeDepartment, HashSet<EmployeePosition>> AllowedPositions =
        new()
        {
            [EmployeeDepartment.HR] = new() { EmployeePosition.Clerk, EmployeePosition.HiringManager },
            [EmployeeDepartment.Engineering] = new() { EmployeePosition.SeniorEngineer, EmployeePosition.TechLead },
        };
    public static bool IsPositionValidForDepartment(EmployeeDepartment department, EmployeePosition position) =>
        AllowedPositions.TryGetValue(department, out var positions) && positions.Contains(position);

    public static readonly HashSet<EmployeePosition> CanCoordinate = new() { EmployeePosition.Clerk };
    public static readonly HashSet<EmployeePosition> CanInterview = new() { EmployeePosition.HiringManager, 
        EmployeePosition.SeniorEngineer,
        EmployeePosition.TechLead, 
        EmployeePosition.Manager, 
        EmployeePosition.HOD};
}
