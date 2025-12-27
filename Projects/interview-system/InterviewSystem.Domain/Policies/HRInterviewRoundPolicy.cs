using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Domain.Policies;

internal static class HRInterviewRoundPolicy
{
    public static InterviewRoundPolicy ClerkPolicy = new()
    {
        AppliedDepartment = EmployeeDepartment.HR,
        AppliedPosition = AppliedPosition.Clerk,
        Steps = new[]
        {
            new InterviewStepPolicy()
            {
                Sequence = 1,
                InterviewerDepartment = EmployeeDepartment.HR,
                AllowedPositions = new HashSet<EmployeePosition>{EmployeePosition.HiringManager},
                IsMandatory = true,
                CanCompleteProcess = false,
                AllowMultiple = false
            },
            new InterviewStepPolicy()
            {
                Sequence = 2,
                InterviewerDepartment = EmployeeDepartment.HR,
                AllowedPositions = new HashSet<EmployeePosition>{EmployeePosition.Manager},
                IsMandatory = true,
                CanCompleteProcess = true,
                AllowMultiple = false
            },
        }
    };
    public static InterviewRoundPolicy TeaLadyPolicy = new()
    {
        AppliedDepartment = EmployeeDepartment.HR,
        AppliedPosition = AppliedPosition.TeaLady,
        Steps = new[]
        {
            new InterviewStepPolicy()
            {
                Sequence = 1,
                InterviewerDepartment = EmployeeDepartment.HR,
                AllowedPositions = new HashSet<EmployeePosition>{EmployeePosition.HiringManager},
                IsMandatory = true,
                CanCompleteProcess = true,
                AllowMultiple = false
            },
        }
    };
}
