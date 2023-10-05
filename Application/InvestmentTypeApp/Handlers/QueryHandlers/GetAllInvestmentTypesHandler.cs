using IncedoInvest.Application.InvestmentTypeApp.Queries;
using IncedoInvest.Domain.Entities;
using IncedoInvest.Domain.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IncedoInvest.Application.InvestmentTypeApp.Handlers
{
    public class GetAllInvestmentTypesHandler : IRequestHandler<GetAllInvestmentTypesQuery, List<InvestmentType>>
    {
        private readonly IInvestmentTypeRepository _repository;

        public GetAllInvestmentTypesHandler(IInvestmentTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<InvestmentType>> Handle(GetAllInvestmentTypesQuery request, CancellationToken cancellationToken)
        {
            var investmentTypes = await _repository.GetAllInvestmentTypesAsync();
            return investmentTypes;
        }
    }
}
