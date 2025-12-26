using InterviewSystem.API.ErrorHandling;
using InterviewSystem.Application.InterviewProcesses.GetInterviewProcessDetails;
using InterviewSystem.Application.InterviewRounds.CreateInterviewRound;
using InterviewSystem.Application.InterviewRounds.GetInterviewRoundsSummary;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InterviewSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InterviewRoundsController : ControllerBase
{
    private readonly IMediator _mediator;

    public InterviewRoundsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetInterviewRounds
        ([FromQuery] GetInterviewRoundsSummaryQuery query)
    {
        var result = await _mediator.Send(query);
        return result.ToActionResult();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetInterviewRound(Guid id)
    {
        var query = new GetInterviewProcessDetailsQuery(id);
        var result = await _mediator.Send(query);
        return result.ToActionResult();
    }

    //[HttpPost]
    //public async Task<IActionResult> CreateInterviewRound
    //    ([FromBody] CreateInterviewRoundCommand command)
    //{
    //    var result = await _mediator.Send(command);
    //    return result.ToActionResult();
    //}
}
