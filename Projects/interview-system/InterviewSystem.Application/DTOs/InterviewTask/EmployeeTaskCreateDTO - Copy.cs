using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Application.DTOs.InterviewTask;

public record EmployeeTaskDetailsDTO(
    Guid Id,
    Guid InterviewRoundId,
    Guid CandidateId,
    InterviewTaskStatus InterviewTaskStatus
    );
