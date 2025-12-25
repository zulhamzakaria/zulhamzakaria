using InterviewSystem.API.ErrorHandling;
using InterviewSystem.Application.InterviewRounds.GetInterviewRoundsSummary;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InterviewSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InterviewRoundController : ControllerBase
{
    private readonly IMediator _mediator;

    public InterviewRoundController(IMediator mediator)
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
}
