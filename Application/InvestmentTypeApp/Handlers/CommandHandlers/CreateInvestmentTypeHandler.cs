using IncedoInvest.Application.InvestmentTypeApp.Commands;
using IncedoInvest.Domain.Entities;
using IncedoInvest.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace IncedoInvest.Application.InvestmentTypeApp.Handlers
{
    public class CreateInvestmentTypeHandler : IRequestHandler<CreateInvestmentTypeCommand, int>
    {
        private readonly IInvestmentTypeRepository _repository;

        public CreateInvestmentTypeHandler(IInvestmentTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateInvestmentTypeCommand request, CancellationToken cancellationToken)
        {
            var investmentType = new InvestmentType
            {
                InvestmentTypeName = request.InvestmentTypeName,
                DeletedFlag = false
            };

            await _repository.AddInvestmentTypeAsync(investmentType);

            return investmentType.InvestmentTypeId;
        }
    }
}
