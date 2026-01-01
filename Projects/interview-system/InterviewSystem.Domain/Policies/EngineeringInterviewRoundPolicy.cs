using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Domain.Policies;

internal static class EngineeringInterviewRoundPolicy
{
    public static InterviewRoundPolicy JuniorEngineerPolicy = new()
    {
        AppliedDepartment = EmployeeDepartment.Engineering,
        AppliedPosition = AppliedPosition.JuniorEngineer,
        Steps = new List<InterviewStepPolicy>
        {
            new InterviewStepPolicy
            {
                Sequence = 1,
                InterviewerDepartment = EmployeeDepartment.HR,
                AllowedPositions = new HashSet<EmployeePosition>{EmployeePosition.HiringManager},
                IsMandatory = true,
                CanCompleteProcess = false,
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
                CanCompleteProcess = true,
                AllowMultiple = false
            }
        }
    };
    public static InterviewRoundPolicy SeniorEngineerPolicy = new()
    {
        AppliedDepartment = EmployeeDepartment.Engineering,
        AppliedPosition = AppliedPosition.SeniorEngineer,
        Steps = new List<InterviewStepPolicy>
        {
            new InterviewStepPolicy
            {
                Sequence = 1,
                InterviewerDepartment = EmployeeDepartment.HR,
                AllowedPositions = new HashSet<EmployeePosition>{EmployeePosition.HiringManager},
                IsMandatory = true,
                CanCompleteProcess = false,
                AllowMultiple = false
            },
            new InterviewStepPolicy
            {
                Sequence = 2,
                InterviewerDepartment = EmployeeDepartment.Engineering,
                AllowedPositions = new HashSet<EmployeePosition> {EmployeePosition.TechLead},
                IsMandatory = true,
                CanCompleteProcess = false,
                AllowMultiple = true
            },
            new InterviewStepPolicy
            {
                Sequence = 3,
                InterviewerDepartment = EmployeeDepartment.Engineering,
                AllowedPositions = new HashSet<EmployeePosition> {EmployeePosition.Manager},
                IsMandatory = true,
                CanCompleteProcess = true,
                AllowMultiple = false
            }
        }
    };
    public static InterviewRoundPolicy TechnicalLead = new()
    {
        AppliedDepartment = EmployeeDepartment.Engineering,
        AppliedPosition = AppliedPosition.TechnicalLead,
        Steps = new List<InterviewStepPolicy>
        {
            new InterviewStepPolicy
            {
                Sequence = 1,
                InterviewerDepartment = EmployeeDepartment.HR,
                AllowedPositions = new HashSet<EmployeePosition>{EmployeePosition.HiringManager},
                IsMandatory = true,
                CanCompleteProcess = false,
                AllowMultiple = false
            },
            new InterviewStepPolicy
            {
                Sequence = 2,
                InterviewerDepartment = EmployeeDepartment.Engineering,
                AllowedPositions = new HashSet<EmployeePosition> {EmployeePosition.Manager},
                IsMandatory = true,
                CanCompleteProcess = false,
                AllowMultiple = true
            },
            new InterviewStepPolicy
            {
                Sequence = 3,
                InterviewerDepartment = EmployeeDepartment.Engineering,
                AllowedPositions = new HashSet<EmployeePosition> {EmployeePosition.HOD},
                IsMandatory = true,
                CanCompleteProcess = true,
                AllowMultiple = false
            }
        }
    };
}

