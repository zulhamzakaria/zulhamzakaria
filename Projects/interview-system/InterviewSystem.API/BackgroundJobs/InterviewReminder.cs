using InterviewSystem.Domain.Interfaces.Repositories;

namespace InterviewSystem.API.BackgroundJobs;

public sealed class InterviewReminder : BackgroundService
{
    private readonly IInterviewTaskRepository _interviewTaskRepository;
    private readonly ILogger<InterviewReminder> _logger;

    public InterviewReminder(IInterviewTaskRepository interviewTaskRepository, 
        ILogger<InterviewReminder> logger)
    {
        _interviewTaskRepository = interviewTaskRepository;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while(stoppingToken.IsCancellationRequested is false)
        {
            try
            {
                await CheckPendingInterviews(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError($"failed to execute InterviewReminder background job: " +
                    $"{ex.InnerException?.Message ?? ex.Message}");
            }

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken); // repeat interval
        }
    }

    private async Task CheckPendingInterviews(CancellationToken ct)
    {
        throw new NotImplementedException();

        //TODO:
        //var pendingTasks = await _interviewTaskRepository.GetTasksPastDueAsync(ct);
        //foreach (var task in pendingTasks)
        //{
        //    // implement what you want to do (e.g., prepare reminder, mark task, etc.)
        //}
    }
}
