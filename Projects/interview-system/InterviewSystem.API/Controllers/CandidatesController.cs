using InterviewSystem.API.ErrorHandling;
using InterviewSystem.Application.Candidates.CreateCandidate;
using InterviewSystem.Application.Candidates.GetCandidateDetails;
using InterviewSystem.Application.Candidates.GetCandidatesSummary;
using InterviewSystem.Application.InterviewProcesses.CreateInterviewProcess;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InterviewSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CandidatesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCandidate([FromBody] CreateCandidateCommand command)
        {
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpPost("{candidateId:guid}/submit-application")]
        public async Task<IActionResult> SubmitApplication(Guid candidateId)
        {
            var command = new CreateInterviewProcessCommand(candidateId);
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCandidateById(Guid id)
        {
            var query = new GetCandidateDetailsQuery(id);
            var result = await _mediator.Send(query);
            return result.ToActionResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetCandidates([FromQuery] GetCandidatesSummaryQuery query)
        {
            var result = await _mediator.Send(query);
            return result.ToActionResult();
        }
    }
}
