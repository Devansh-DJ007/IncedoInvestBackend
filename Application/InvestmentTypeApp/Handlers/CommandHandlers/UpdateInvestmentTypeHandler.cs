using IncedoInvest.Application.InvestmentTypeApp.Commands;
using IncedoInvest.Domain.Entities;
using IncedoInvest.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace IncedoInvest.Application.InvestmentTypeApp.Handlers
{
    public class UpdateInvestmentTypeHandler : IRequestHandler<UpdateInvestmentTypeCommand, bool>
    {
        private readonly IInvestmentTypeRepository _repository;

        public UpdateInvestmentTypeHandler(IInvestmentTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateInvestmentTypeCommand request, CancellationToken cancellationToken)
        {
            var existingInvestmentType = await _repository.GetInvestmentTypeByIdAsync(request.InvestmentTypeId);

            if (existingInvestmentType == null)
            {
                return false;
            }

            existingInvestmentType.InvestmentTypeName = request.InvestmentTypeName;

            await _repository.UpdateInvestmentTypeAsync(existingInvestmentType);

            return true;
        }
    }
}
