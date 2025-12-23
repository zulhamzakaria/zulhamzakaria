using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;
using System.Reflection.Metadata.Ecma335;

namespace InterviewSystem.Application.InterviewProcesses.GetInterviewProcessesSummary;

public sealed class GetInterviewProcessesSummaryHandler
{
    private readonly IInterviewProcessRepository _interviewProcessRepository;

    public GetInterviewProcessesSummaryHandler(IInterviewProcessRepository interviewProcessRepository)
    {
        _interviewProcessRepository = interviewProcessRepository;
    }

    public async Task<Result<IReadOnlyCollection<GetInterviewProcessesSummaryDTO>>> HandleAsync(GetInterviewProcessesSummaryQuery query)
    {
        var results = (await _interviewProcessRepository.GetAllAsync())
                .Where(ip => query.EmployeeDepartment == null || ip.Department == query.EmployeeDepartment)
                .Where(ip => query.InterviewProcessStatus == null || ip.InterviewProcessStatus == query.InterviewProcessStatus)
                .Where(ip => query.InterviewerId == null || ip.CurrentInterviewerId == query.InterviewerId)
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
           process.CurrentSequence,
           process.InterviewProcessStatus,
           process.CurrentInterviewerId ,
           process.CurrentInterviewerName
           ); 
}
