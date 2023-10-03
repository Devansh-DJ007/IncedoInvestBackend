using IncedoInvest.Application.AdvisorApp.Commands;
using IncedoInvest.Application.AdvisorApp.Queries;
using IncedoInvest.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IncedoInvest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvisorController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAdvisorRepository _advisorRepository;

        public AdvisorController(IMediator mediator, IAdvisorRepository advisorRepository)
        {
            _mediator = mediator;
            _advisorRepository = advisorRepository;
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] LoginAdvisorCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(new { Token = result.Data });
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterAdvisorCommand command)
        {
            var advisorExists = await _advisorRepository.AdvisorExistsAsync(command.Email);
            if (advisorExists)
            {
                return BadRequest("Email already exists");
            }

            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok("Registration successful");
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet("GetAdvisor")]
        public async Task<IActionResult> GetAdvisor([FromQuery] GetAllAdvisorQuery query)
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

        [HttpPut("UpdateAdvisor/{advisorEmail}")]
        public async Task<IActionResult> UpdateAdvisor([FromBody] UpdateAdvisorCommand command)
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

        [HttpDelete("Advisor/{advisorId}")]
        public async Task<IActionResult> DeleteUser(int advisorId)
        {
            var command = new DeleteAdvisorCommand { AdvisorId = advisorId };
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
