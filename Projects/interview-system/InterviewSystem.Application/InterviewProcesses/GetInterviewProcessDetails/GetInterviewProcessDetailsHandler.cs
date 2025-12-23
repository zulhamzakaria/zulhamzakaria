using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;

namespace InterviewSystem.Application.InterviewProcesses.GetInterviewProcessDetails;

public sealed class GetInterviewProcessDetailsHandler
{ 
    private readonly IInterviewProcessRepository _interviewProcessRepository;

    public GetInterviewProcessDetailsHandler(IInterviewProcessRepository interviewProcessRepository)
    {
        _interviewProcessRepository = interviewProcessRepository;
    }

    public async Task<Result<GetInterviewProcessDetailsDTO>> HandleAsync(GetInterviewProcessDetailsQuery query)
    {
        var result = await _interviewProcessRepository.GetByIdAsync(query.Id);

        if(result is null)
            return Result<GetInterviewProcessDetailsDTO>.Failure(GenericErrors.NoRecordFound(nameof(InterviewProcess), query.Id));

        var returnResult = MapToDTO(result);

        return Result<GetInterviewProcessDetailsDTO>.Success(returnResult);

    }

    private GetInterviewProcessDetailsDTO MapToDTO(InterviewProcess process) =>
        new GetInterviewProcessDetailsDTO(process.Id,
            process.CandidateId,
            process.Department,
            process.CurrentSequence,
            process.InterviewProcessStatus,
            process.CurrentInterviewerId,
            process.CurrentInterviewerName);
}
