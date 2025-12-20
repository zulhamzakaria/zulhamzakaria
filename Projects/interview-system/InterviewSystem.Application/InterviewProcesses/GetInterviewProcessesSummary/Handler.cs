using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;
using System.Reflection.Metadata.Ecma335;

namespace InterviewSystem.Application.InterviewProcesses.GetInterviewProcessesSummary;

public sealed class Handler
{
    private readonly IInterviewProcessRepository _interviewProcessRepository;

    public Handler(IInterviewProcessRepository interviewProcessRepository)
    {
        _interviewProcessRepository = interviewProcessRepository;
    }

    public async Task<Result<IReadOnlyCollection<DTO>>> HandleAsync(Query query)
    {
        var results = (await _interviewProcessRepository.GetAllAsync())
                .Where(ip => query.EmployeeDepartment == null || ip.Department == query.EmployeeDepartment)
                .Where(ip => query.InterviewProcessStatus == null || ip.InterviewProcessStatus == query.InterviewProcessStatus)
                .Where(ip => query.InterviewerId == null || ip.CurrentInterviewerId == query.InterviewerId)
                .Select(MapToDTO)
                .ToList();

        if (results.Any() is false)
            return Result<IReadOnlyCollection<DTO>>.Failure(GenericErrors.NoRecordsFound(nameof(InterviewProcess)));

        return Result<IReadOnlyCollection<DTO>>.Success(results);

    }

    private DTO MapToDTO(InterviewProcess process) =>
       new DTO(
           process.Id,
           process.CandidateId,
           process.CandidateName,
           process.CurrentSequence,
           process.InterviewProcessStatus,
           process.CurrentInterviewerId ,
           process.CurrentInterviewerName
           ); 
}
