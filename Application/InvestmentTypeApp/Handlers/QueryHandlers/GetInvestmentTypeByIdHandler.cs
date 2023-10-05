using IncedoInvest.Application.InvestmentTypeApp.Queries;
using IncedoInvest.Domain.Entities;
using IncedoInvest.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace IncedoInvest.Application.InvestmentTypeApp.Handlers
{
    public class GetInvestmentTypeByIdHandler : IRequestHandler<GetInvestmentTypeByIdQuery, InvestmentType>
    {
        private readonly IInvestmentTypeRepository _repository;

        public GetInvestmentTypeByIdHandler(IInvestmentTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<InvestmentType> Handle(GetInvestmentTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var investmentType = await _repository.GetInvestmentTypeByIdAsync(request.InvestmentTypeId);
            return investmentType;
        }
    }
}
