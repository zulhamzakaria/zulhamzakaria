using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Application.InterviewProcesses.GetInterviewProcessDetails;

public sealed record GetInterviewProcessDetailsDTO(Guid Id,
    Guid CandidateId,
    EmployeeDepartment EmployeeDepartment,
    int CurrentSequence,
    InterviewProcessStatus InterviewProcessStatus,
    Guid? CurrentInterviewId,
    string? CurrentInterviewName);
