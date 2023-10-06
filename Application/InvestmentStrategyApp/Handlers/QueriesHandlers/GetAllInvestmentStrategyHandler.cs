using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IncedoInvest.Domain.Entities;
using IncedoInvest.Domain.Interfaces;
using MediatR;

namespace IncedoInvest.Application.InvestmentStrategyApp.Queries
{
    public class GetAllInvestmentStrategyQueryHandler : IRequestHandler<GetAllInvestmentStrategyQuery, List<InvestmentStrategy>>
    {
        private readonly IInvestmentStrategyRepository _repository;

        public GetAllInvestmentStrategyQueryHandler(IInvestmentStrategyRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<InvestmentStrategy>> Handle(GetAllInvestmentStrategyQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var investmentStrategies = await _repository.GetAllStrategyAsync();
                return investmentStrategies;
            }
            catch (Exception ex)
            {
                // You can handle exceptions here or log them if needed
                throw new ApplicationException("Error fetching investment strategies", ex);
            }
        }
    }
}
