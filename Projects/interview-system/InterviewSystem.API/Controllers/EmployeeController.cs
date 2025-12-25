using InterviewSystem.API.ErrorHandling;
using InterviewSystem.Application.Employees.CreateEmployee;
using InterviewSystem.Application.Employees.GetEmployeeDetails;
using InterviewSystem.Application.Employees.GetEmployeesSummary;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InterviewSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees([FromQuery] GetEmployeesSummaryQuery query)
        {
            var result = await _mediator.Send(query);
            return result.ToActionResult();
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            var query = new GetEmployeeDetailsQuery(id);
            var result = await _mediator.Send(query);
            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command)
        {
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }
    }
}
