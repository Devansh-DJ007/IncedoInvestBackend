using IncedoInvest.Application.ProposedInvestmentApp.Commands;
using IncedoInvest.Application.ProposedInvestmentApp.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IncedoInvest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProposedInvestmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProposedInvestmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateProposedInvestmentCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok("ProposedInvestment added successfully");
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetUsers([FromQuery] GetAllPropsedInvestmentsQuery query)
        {
            var proposedInvestments = await _mediator.Send(query);

            if (proposedInvestments is not null)
            {
                return Ok(proposedInvestments);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
