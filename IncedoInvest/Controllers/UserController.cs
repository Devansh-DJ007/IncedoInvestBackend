using IncedoInvest.Application.Services;
using IncedoInvest.Application.UserApp.Commands;
using IncedoInvest.Application.UserApp.Queries;
using IncedoInvest.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
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
        public async Task<IActionResult> GetUsers([FromQuery] GetAllUsersQuery query)
        {
            var users = await _mediator.Send(query);

            if (users is not null)
            {
                return Ok(users);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("UsersByRole/{roleId}")]
        public async Task<IActionResult> GetUsersByRoleId(int roleId)
        {
            var query = new GetUsersByRoleIDQuery { roleId = roleId };
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("ClientsByAdvisor/{advisorId}")]
        public async Task<IActionResult> GetClientsByAdvisorId(string advisorId)
        {
            var query = new GetClientsByAdvisorIdQuery { advisorId = advisorId };
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("GetInvestmentAmountForClient/{userId}")]
        public async Task<IActionResult> GetInvestmentAmountForClient(int userId)
        {
            var query = new GetInvestmentAmountForClientQuery(userId);
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("AdvisorDashboard/{advisorId}")]
        public async Task<IActionResult> GetAdvisorDashboard(string advisorId)
        {
            var query = new GetAdvisorDashboardQuery { AdvisorId = advisorId };
            var result = await _mediator.Send(query);

            if (result is not null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("ClientDashboard/{clientId}")]
        public async Task<IActionResult> GetClientDashboard(string clientId)
        {
            var query = new GetClientDashboardQuery { ClientId = clientId };
            var result = await _mediator.Send(query);

            if (result is not null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("AcceptProposedInvestment")]
        public async Task<IActionResult> AcceptProposedInvestment([FromBody] AcceptProposedInvestmentCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok("Proposed Investment accepted successfully");
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("users/{userId}")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
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

        [HttpDelete("User/{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var command = new DeleteUserCommand { UserId = userId };
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
