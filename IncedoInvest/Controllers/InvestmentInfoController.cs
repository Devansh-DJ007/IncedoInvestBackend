using IncedoInvest.Application.InvestmentInfoApp.Commands;
using IncedoInvest.Application.InvestmentInfoApp.Queries;
using IncedoInvest.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IncedoInvest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestmentInfoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IInvestmentInfoRepository _investmentInfoRepository;

        public InvestmentInfoController(IMediator mediator, IInvestmentInfoRepository investmentInfoRepository)
        {
            _mediator = mediator;
            _investmentInfoRepository = investmentInfoRepository;
        }

        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateInvestmentInfo([FromBody] CreateInvestmentInfoCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                if (result.IsSuccess)
                {
                    return Ok("Registration successful");
                }
                else
                {
                    return BadRequest(result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating InvestmentInfo: {ex.Message}");
            }
        }


        [HttpPut("Update/{investmentInfoId}")]
        public async Task<IActionResult> UpdateInvestmentInfo(int investmentInfoId, [FromBody] UpdateInvestmentInfoCommand command)
        {
            try
            {
                command.InvestmentInfoId = investmentInfoId;
                var result = await _mediator.Send(command);

                if (result.IsSuccess)
                {
                    return NoContent();
                }
                else if (result.ErrorMessage == "InvestmentInfo not found")
                {
                    return NotFound(result.ErrorMessage);
                }
                else
                {
                    return BadRequest(result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating InvestmentInfo: {ex.Message}");
            }
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllInvestmentInfo()
        {
            try
            {
                var query = new GetAllInvestmentInfoQuery();
                var result = await _mediator.Send(query);

                if (result.IsSuccess)
                {
                    return Ok(result.Data);
                }
                else
                {
                    return BadRequest(result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving InvestmentInfo: {ex.Message}");
            }
        }


        [HttpDelete("Delete/{investmentInfoId}")]
        public async Task<IActionResult> DeleteInvestmentInfo(int investmentInfoId)
        {
            try
            {
                var command = new DeleteInvestmentInfoCommand { InvestmentInfoId = investmentInfoId };
                var result = await _mediator.Send(command);

                if (result.IsSuccess)
                {
                    return NoContent();
                }
                else if (result.ErrorMessage == "InvestmentInfo not found")
                {
                    return NotFound(result.ErrorMessage);
                }
                else
                {
                    return BadRequest(result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting InvestmentInfo: {ex.Message}");
            }
        }
    }
}
