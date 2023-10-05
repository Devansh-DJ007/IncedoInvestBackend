using IncedoInvest.Domain.Entities;
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
    public class InvestmentTypeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IInvestmentTypeRepository _investmentTypeRepository;

        public InvestmentTypeController(IMediator mediator, IInvestmentTypeRepository investmentTypeRepository)
        {
            _mediator = mediator;
            _investmentTypeRepository = investmentTypeRepository;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var investmentTypes = await _investmentTypeRepository.GetAllInvestmentTypesAsync();

                if (investmentTypes != null)
                {
                    return Ok(investmentTypes);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error 1 occurred: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var investmentType = await _investmentTypeRepository.GetInvestmentTypeByIdAsync(id);

                if (investmentType != null)
                {
                    return Ok(investmentType);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error 2 occurred: {ex.Message}");
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] InvestmentType investmentType)
        {
            try
            {
                await _investmentTypeRepository.AddInvestmentTypeAsync(investmentType);
                return CreatedAtAction(nameof(GetById), new { id = investmentType.InvestmentTypeId }, investmentType);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error 3 occurred: {ex.Message}");
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] InvestmentType investmentType)
        {
            try
            {
                if (id != investmentType.InvestmentTypeId)
                {
                    return BadRequest();
                }

                await _investmentTypeRepository.UpdateInvestmentTypeAsync(investmentType);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error 4 occurred: {ex.Message}");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var investmentType = await _investmentTypeRepository.GetInvestmentTypeByIdAsync(id);

                if (investmentType == null)
                {
                    return NotFound();
                }

                await _investmentTypeRepository.DeleteInvestmentTypeAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error 5 occurred: {ex.Message}");
            }
        }
    }
}
