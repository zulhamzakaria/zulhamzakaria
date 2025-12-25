using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace InterviewSystem.Application.InterviewProcesses.GetInterviewProcessesSummary;

public sealed class GetInterviewProcessesSummaryHandler :
     IRequestHandler<GetInterviewProcessesSummaryQuery, Result<IReadOnlyCollection<GetInterviewProcessesSummaryDTO>>>
{
    private readonly IInterviewProcessRepository _interviewProcessRepository;

    public GetInterviewProcessesSummaryHandler(IInterviewProcessRepository interviewProcessRepository)
    {
        _interviewProcessRepository = interviewProcessRepository;
    }

    public async Task<Result<IReadOnlyCollection<GetInterviewProcessesSummaryDTO>>> Handle
        (GetInterviewProcessesSummaryQuery request, CancellationToken cancellationToken)
    {
        var results = (await _interviewProcessRepository.GetAllAsync())
                .Where(ip => request.EmployeeDepartment == null || ip.Department == request.EmployeeDepartment)
                .Where(ip => request.InterviewProcessStatus == null || ip.InterviewProcessStatus == request.InterviewProcessStatus)
                .Where(ip => request.InterviewerId == null || ip.CurrentInterviewerId == request.InterviewerId)
                .Select(MapToDTO)
                .ToList();

        if (results.Any() is false)
            return Result<IReadOnlyCollection<GetInterviewProcessesSummaryDTO>>.Failure(GenericErrors.NoRecordsFound(nameof(InterviewProcess)));

        return Result<IReadOnlyCollection<GetInterviewProcessesSummaryDTO>>.Success(results);
    }

    private GetInterviewProcessesSummaryDTO MapToDTO(InterviewProcess process) =>
       new GetInterviewProcessesSummaryDTO(
           process.Id,
           process.CandidateId,
           process.CandidateName,
           process.CurrentRoundSequence,
           process.InterviewProcessStatus,
           process.CurrentInterviewerId,
           process.CurrentInterviewerName
           );
}
