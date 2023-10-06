using IncedoInvest.Application.AdvisorApp.Queries;
using IncedoInvest.Application.InvestmentStrategyApp.Queries;
using IncedoInvest.Domain.Entities;
using IncedoInvest.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncedoInvest.Application.InvestmentStrategyApp.Handler.QueriesHandler
{
    public class GetAllInvestmentStrategyHandler : IRequestHandler<GetAllInvestmentStrategyQuery, List<InvestmentStrategy>>
    {
        private readonly IInvestmentStrategyRepository _investmentRepository;
        private readonly IConfiguration _configuration;

        public GetAllInvestmentStrategyHandler(IInvestmentStrategyRepository investment, IConfiguration configuration)
        {
            _investmentRepository = investment;
            _configuration = configuration;
        }

   
  
        public Task<List<InvestmentStrategy>> Handle(GetAllInvestmentStrategyQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
