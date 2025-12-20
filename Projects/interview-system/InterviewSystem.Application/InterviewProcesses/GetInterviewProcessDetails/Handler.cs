using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;

namespace InterviewSystem.Application.InterviewProcesses.GetInterviewProcessDetails;

public sealed class Handler
{ 
    private readonly IInterviewProcessRepository _interviewProcessRepository;

    public Handler(IInterviewProcessRepository interviewProcessRepository)
    {
        _interviewProcessRepository = interviewProcessRepository;
    }

    public async Task<Result<DTO>> HandleAsync(Query query)
    {
        var result = await _interviewProcessRepository.GetByIdAsync(query.Id);

        if(result is null)
            return Result<DTO>.Failure(GenericErrors.NoRecordFound(nameof(InterviewProcess), query.Id));

        var returnResult = MapToDTO(result);

        return Result<DTO>.Success(returnResult);

    }

    private DTO MapToDTO(InterviewProcess process) =>
        new DTO(process.Id,
            process.CandidateId,
            process.Department,
            process.CurrentSequence,
            process.InterviewProcessStatus,
            process.CurrentInterviewerId,
            process.CurrentInterviewerName);
}
