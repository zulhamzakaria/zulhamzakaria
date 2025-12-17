using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Application.DTOs.InterviewProcess;

public record InterviewProcessDetailsDTO(
    Guid Id,
    Guid CandidateId,
    EmployeeDepartment EmployeeDepartment,
    int CurrentSequence,
    InterviewProcessStatus InterviewProcessStatus,
    Guid CurrentInterviewId,
    string CurrentInterviewName
    );
