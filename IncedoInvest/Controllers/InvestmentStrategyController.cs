using static System.Net.Mime.MediaTypeNames;
using IncedoInvest.Application.ClientApp.Commands;
using IncedoInvest.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IncedoInvest.Application.InvestmentStrategyApp.Command;
using IncedoInvest.Application.InvestmentStrategyApp.Queries;

namespace IncedoInvest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestmentStrategyController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IInvestmentStrategyRepository _investmentStrategyRepository;

        public InvestmentStrategyController(IMediator mediator, IInvestmentStrategyRepository investmentStrategyRepository)
        {
            _mediator = mediator;
            _investmentStrategyRepository = investmentStrategyRepository;
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateInvestmentStrategyCommand command)
        {
          
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok("Create Strategy");
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet("GetStrategy")]
        public async Task<IActionResult> GetAdvisor([FromQuery] GetAllInvestmentStrategyQuery query)
        {
            var advisors = await _mediator.Send(query);

            if (advisors is not null)
            {
                return Ok(advisors);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateInvestmentStrategy([FromBody] UpdateInvestmentStrategyCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return NoContent();
            }
            else if (result.ErrorMessage == "User not found")
            {
                return NotFound(result.ErrorMessage);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpDelete("Strategy/{Id}")]
        public async Task<IActionResult> DeleteUser(int invstId)
        {
            var command = new DeleteInvestmentStrategyCommand { InvestmentId = invstId };
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
