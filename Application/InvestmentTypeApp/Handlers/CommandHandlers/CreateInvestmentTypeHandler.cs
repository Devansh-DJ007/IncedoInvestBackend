using IncedoInvest.Application.InvestmentTypeApp.Commands;
using IncedoInvest.Application.Services;
using IncedoInvest.Domain.Entities;
using IncedoInvest.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace IncedoInvest.Application.InvestmentTypeApp.Handlers
{
    public class CreateInvestmentTypeHandler : IRequestHandler<CreateInvestmentTypeCommand, Result<string>>
    {
        private readonly IInvestmentTypeRepository _repository;

        public CreateInvestmentTypeHandler(IInvestmentTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<string>> Handle(CreateInvestmentTypeCommand request, CancellationToken cancellationToken)
        {
            var investmentType = new InvestmentType
            {
                InvestmentTypeName = request.InvestmentTypeName,
                DeletedFlag = false
            };

            await _repository.AddInvestmentTypeAsync(investmentType);

            return Result<string>.Success("InvestmentType Added Successfully");
        }
    }
}
