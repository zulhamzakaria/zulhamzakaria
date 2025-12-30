using InterviewSystem.Domain.Interfaces.Repositories;

namespace InterviewSystem.API.BackgroundJobs;

public sealed class InterviewReminder : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<InterviewReminder> _logger;

    public InterviewReminder(ILogger<InterviewReminder> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while(stoppingToken.IsCancellationRequested is false)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var taskRepo = scope.ServiceProvider.GetRequiredService<IInterviewTaskRepository>();

                var now = DateTimeOffset.UtcNow;
                var nextRun = now.Date.AddDays(1);
                var delay = nextRun - now;

                await Task.Delay(delay, stoppingToken);

                int maxRetries = 5;

                await RetryAsync(
                     async() => await CheckPendingInterviews(taskRepo, stoppingToken),
                     maxRetries,
                     TimeSpan.FromMinutes(5),
                     stoppingToken);
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError(ex.InnerException?.Message ?? ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"failed to execute InterviewReminder background job: " +
                    $"{ex.InnerException?.Message ?? ex.Message}");
            }
        }
    }

    private async Task CheckPendingInterviews(IInterviewTaskRepository taskRepo, CancellationToken ct)
    {
        throw new NotImplementedException();

        //TODO:
        //var pendingTasks = await _interviewTaskRepository.GetTasksPastDueAsync(ct);
        //foreach (var task in pendingTasks)
        //{
        //    // implement what you want to do (e.g., prepare reminder, mark task, etc.)
        //}
    }

    private async Task RetryAsync(Func<Task> action, int maxRetries, TimeSpan retryDelay, CancellationToken ct)
    {
        int attempt = 0;
        while (attempt < maxRetries)
        {
            try
            {
                await action();
                return;
            }
            catch
            {
                attempt++;
                if (attempt >= maxRetries) throw;
                await Task.Delay(retryDelay, ct);
            }
        }
    }
}
