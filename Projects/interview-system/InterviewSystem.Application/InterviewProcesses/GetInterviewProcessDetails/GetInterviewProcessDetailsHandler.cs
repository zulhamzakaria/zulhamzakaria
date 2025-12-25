using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace InterviewSystem.Application.InterviewProcesses.GetInterviewProcessDetails;

public sealed class GetInterviewProcessDetailsHandler :
    IRequestHandler<GetInterviewProcessDetailsQuery, Result<GetInterviewProcessDetailsDTO>>
{
    private readonly IInterviewProcessRepository _interviewProcessRepository;

    public GetInterviewProcessDetailsHandler(IInterviewProcessRepository interviewProcessRepository)
    {
        _interviewProcessRepository = interviewProcessRepository;
    }

    public async Task<Result<GetInterviewProcessDetailsDTO>> Handle(GetInterviewProcessDetailsQuery request, CancellationToken cancellationToken)
    {
        var result = await _interviewProcessRepository.GetByIdAsync(request.Id);

        if (result is null)
            return Result<GetInterviewProcessDetailsDTO>.Failure(GenericErrors.NoRecordFound(nameof(InterviewProcess), request.Id));

        var returnResult = MapToDTO(result);

        return Result<GetInterviewProcessDetailsDTO>.Success(returnResult);
    }


    private GetInterviewProcessDetailsDTO MapToDTO(InterviewProcess process) =>
        new GetInterviewProcessDetailsDTO(process.Id,
            process.CandidateId,
            process.Department,
            process.CurrentRoundSequence,
            process.InterviewProcessStatus,
            process.CurrentInterviewerId,
            process.CurrentInterviewerName);
}
