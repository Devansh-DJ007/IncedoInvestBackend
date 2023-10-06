using System.Threading;
using System.Threading.Tasks;
using IncedoInvest.Application.InvestmentStrategyApp.Command;
using IncedoInvest.Application.Services;
using IncedoInvest.Domain.Entities;
using IncedoInvest.Domain.Interfaces;
using MediatR;

namespace IncedoInvest.Application.InvestmentStrategyApp.CommandHandlers
{
    public class CreateInvestmentStrategyCommandHandler : IRequestHandler<CreateInvestmentStrategyCommand, Result<string>>
    {
        private readonly IInvestmentStrategyRepository _repository;

        public CreateInvestmentStrategyCommandHandler(IInvestmentStrategyRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<string>> Handle(CreateInvestmentStrategyCommand request, CancellationToken cancellationToken)
        {
            var existingStrategy = await _repository.GetStrategyByNameAsync(request.InvestmentStrategyName);

            if (existingStrategy != null)
            {
                return Result<string>.Fail("Investment Strategy with the same name already exists.");
            }
            var investmentStrategy = new InvestmentStrategy
            {
                InvestmentStrategyName = request.InvestmentStrategyName,
               InvestmentTypeId = request.InvestmentTypeId,
                Return10Y = (double)request.Return10Y,
                Risk10Y = (double)request.Risk10Y,
                Return1Y = (double)request.Return1Y,
                DeletedFlag = request.DeletedFlag
            };

            await _repository.AddStrategyAsync(investmentStrategy);
            return Result<string>.Success("Succesfully Added INvestment Strategy");
        }

      
    }
}

