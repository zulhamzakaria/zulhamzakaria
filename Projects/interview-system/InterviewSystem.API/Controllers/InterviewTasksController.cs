using InterviewSystem.API.ErrorHandling;
using InterviewSystem.Application.InterviewTasks.CreateInterviewTask;
using InterviewSystem.Application.InterviewTasks.Evaluation.CompleteInterview;
using InterviewSystem.Application.InterviewTasks.Evaluation.RejectInterview;
using InterviewSystem.Application.InterviewTasks.GetInterviewTaskDetails;
using InterviewSystem.Application.InterviewTasks.GetInterviewTasksSummary;
using InterviewSystem.Application.InterviewTasks.Scheduling.RejectTask;
using InterviewSystem.Application.InterviewTasks.Scheduling.Rescheduling;
using InterviewSystem.Application.InterviewTasks.Scheduling.SetInterviewDate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InterviewSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InterviewTasksController : ControllerBase
{
    private readonly IMediator _mediator;
    public InterviewTasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetInterviewTasks
        ([FromQuery] GetInterviewTasksSummaryQuery queries)
    {
        var result = await _mediator.Send(queries);
        return result.ToActionResult();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetInterviewTask(Guid id)
    {
        var query = new GetInterviewTaskDetailsQuery(id);
        var result = await _mediator.Send(query);
        return result.ToActionResult();
    }

    [HttpPost("schedule-interview")]
    public async Task<IActionResult> ScheduleInterview([FromBody] SetInterviewDateCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPost("reschedule-interview")]
    public async Task<IActionResult> RecheduleInterview([FromBody] ReschedulingCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPost("passed-interview")]
    public async Task<IActionResult> PassInterview([FromBody] PassInterviewCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPost("failed-interview")]
    public async Task<IActionResult> FailedInterview([FromBody] FailInterviewCommand command)
    {
        var result  = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPost("reject-task")]
    public async Task<IActionResult> RejectTask([FromBody] RejectTaskCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }
}
