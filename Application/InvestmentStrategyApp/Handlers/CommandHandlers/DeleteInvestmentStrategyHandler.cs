using System;
using System.Threading;
using System.Threading.Tasks;
using IncedoInvest.Application.Services;
using IncedoInvest.Domain.Interfaces;
using MediatR;

namespace IncedoInvest.Application.InvestmentStrategyApp.Command
{
    public class DeleteInvestmentStrategyCommandHandler : IRequestHandler<DeleteInvestmentStrategyCommand, Result<string>>
    {
        private readonly IInvestmentStrategyRepository _repository;

        public DeleteInvestmentStrategyCommandHandler(IInvestmentStrategyRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<string>> Handle(DeleteInvestmentStrategyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var investmentStrategy = await _repository.GetStrategyByIdAsync(request.InvestmentId);

                if (investmentStrategy == null)
                {
                    return Result<string>.Fail("Investment strategy not found");
                }

                await _repository.DeleteStrategyAsync(request.InvestmentId);

                return Result<string>.Success("Investment strategy deleted successfully");
            }
            catch (Exception ex)
            {
                return Result<string>.Fail(ex.Message);
            }
        }
    }
}
