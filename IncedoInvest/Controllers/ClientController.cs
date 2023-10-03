using IncedoInvest.Application.AdvisorApp.Commands;
using IncedoInvest.Application.ClientApp.Commands;
using IncedoInvest.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IncedoInvest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IClientRepository _clientRepository;

        public ClientController(IMediator mediator, IClientRepository clientRepository)
        {
            _mediator = mediator;
            _clientRepository = clientRepository;
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] LoginClientCommand command)
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
        public async Task<IActionResult> Register([FromBody] RegisterClientCommand command)
        {
            var clientExists = await _clientRepository.ClientExistsAsync(command.Email);
            if (clientExists)
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
    }
}
