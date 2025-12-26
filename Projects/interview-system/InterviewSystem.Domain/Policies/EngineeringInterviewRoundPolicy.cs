using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Domain.Policies;

public static class EngineeringInterviewRoundPolicy
{
    public static InterviewRoundPolicy Policy = new()
    {
        AppliedDepartment = EmployeeDepartment.Engineering,
        Steps =
        [
            new InterviewStepPolicy
            {
                Sequence = 1,
                InterviewerDepartment = EmployeeDepartment.HR,
                AllowedPositions = new HashSet<EmployeePosition>{EmployeePosition.HiringManager},
                IsMandatory = true,
                CanCompleteProcess = true,
                AllowMultiple = false
            },
            new InterviewStepPolicy
            {
                Sequence = 2,
                InterviewerDepartment = EmployeeDepartment.Engineering,
                AllowedPositions = new HashSet<EmployeePosition> {EmployeePosition.SeniorEngineer},
                IsMandatory = true,
                CanCompleteProcess = false,
                AllowMultiple = true
            },
            new InterviewStepPolicy
            {
                Sequence = 3,
                InterviewerDepartment = EmployeeDepartment.Engineering,
                AllowedPositions = new HashSet<EmployeePosition> {EmployeePosition.TechLead},
                IsMandatory = true,
                CanCompleteProcess = false,
                AllowMultiple = true
            },
            new InterviewStepPolicy
            {
                Sequence = 4,
                InterviewerDepartment = EmployeeDepartment.Engineering,
                AllowedPositions = new HashSet<EmployeePosition> {EmployeePosition.Manager},
                IsMandatory = true,
                CanCompleteProcess = false,
                AllowMultiple = false
            },
            new InterviewStepPolicy
            {
                Sequence = 5,
                InterviewerDepartment = EmployeeDepartment.Engineering,
                AllowedPositions = new HashSet<EmployeePosition> {EmployeePosition.HOD},
                IsMandatory = true,
                CanCompleteProcess = true,
                AllowMultiple = false
            },
        ]
    };
}
