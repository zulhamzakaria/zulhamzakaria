using InterviewSystem.API.ErrorHandling;
using InterviewSystem.Application.InterviewProcesses.CreateInterviewProcess;
using InterviewSystem.Application.InterviewProcesses.GetInterviewProcessDetails;
using InterviewSystem.Application.InterviewProcesses.GetInterviewProcessesSummary;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InterviewSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterviewProcessesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public InterviewProcessesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetInterviewProcesses
            ([FromQuery] GetInterviewProcessesSummaryQuery query)
        {
            var result = await _mediator.Send(query);
            return result.ToActionResult();
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetInterviewProcess(Guid id)
        {
            var query = new GetInterviewProcessDetailsQuery(id);
            var result = await _mediator.Send(query);
            return result.ToActionResult();
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateInterviewProcess
        //    ([FromBody] CreateInterviewProcessCommand command)
        //{
        //    var result = await _mediator.Send(command);
        //    return result.ToActionResult();
        //}

    }
}
