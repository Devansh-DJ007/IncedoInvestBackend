

using IncedoInvest.Application.InvestmentStrategyApp.Command;
using IncedoInvest.Application.Services;
using IncedoInvest.Domain.Entities;
using IncedoInvest.Domain.Interfaces;
using MediatR;

namespace IncedoInvest.Application.InvestmentStrategyApp.CommandHandlers
{
    public class UpdateInvestmentStrategyCommandHandler : IRequestHandler<UpdateInvestmentStrategyCommand, Result<InvestmentStrategy>>
    {
        private readonly IInvestmentStrategyRepository _repository;

        public UpdateInvestmentStrategyCommandHandler(IInvestmentStrategyRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<InvestmentStrategy>> Handle(UpdateInvestmentStrategyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingStrategy = await _repository.GetStrategyByIdAsync(request.InvestmentStrategyId);

                if (existingStrategy == null)
                {
                    return Result<InvestmentStrategy>.Fail("Investment strategy not found");
                }

                // Update the properties of the existing strategy
                existingStrategy.InvestmentStrategyName = request.InvestmentStrategyName;
                existingStrategy.InvestmentTypeId = request.InvestmentStrategyId;
                existingStrategy.Return10Y = request.Return10Y;
                existingStrategy.Risk10Y = request.Risk10Y;
                existingStrategy.Return1Y = request.Return1Y;
                existingStrategy.DeletedFlag = request.DeletedFlag;

                // Optionally, you can update the InvestmentType as well
                // existingStrategy.InvestmentType = request.InvestmentType;

                await _repository.UpdateStrategyAsync(existingStrategy);

                return Result<InvestmentStrategy>.Success(existingStrategy);
            }
            catch (Exception ex)
            {
                return Result<InvestmentStrategy>.Fail(ex.InnerException.Message);
            } // You can modify the return value as needed.
        }
    }
}
    


