using IncedoInvest.Application.Commands;
using IncedoInvest.Application.Queries;
using IncedoInvest.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace IncedoInvest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserRepository _userRepository;

        public UserController(IMediator mediator, IUserRepository userRepository)
        {
            _mediator = mediator;
            _userRepository = userRepository;
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
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            var userExists = await _userRepository.UserExistsAsync(command.Email);
            if (userExists)
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

        [HttpGet("Users")]
        public async Task<IActionResult> GetUsers([FromQuery] GetUsersQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("UsersByRole/{roleId}")]
        public async Task<IActionResult> GetUsersByRoleId([FromQuery] GetUsersByRoleIDQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}
