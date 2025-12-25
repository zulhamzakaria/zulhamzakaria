using InterviewSystem.API.ErrorHandling;
using InterviewSystem.Application.InterviewTasks.GetInterviewTasksSummary;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InterviewSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InterviewTaskController : ControllerBase
{
    private readonly IMediator _mediator;
    public InterviewTaskController(IMediator mediator)
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
}
