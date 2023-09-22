using IncedoInvest.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IncedoInvest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvisorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdvisorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] LoginAdvisorCommand command)
        {
            var result = await _mediator.Send(command);

            return (IActionResult)result;
            //if (result.IsSuccess)
            //{
            //    return Ok(new { Token = result.Data });
            //}
            //else
            //{
            //    return Unauthorized(); // Invalid username or password
            //}
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterAdvisorCommand command)
        {
            var result = await _mediator.Send(command);

            return (IActionResult)result;
            //if (result.IsSuccess) 
            //{
            //    return Ok("Registration successful");
            //}
            //else
            //{
            //    return BadRequest();
            //}
        }
    }
}
