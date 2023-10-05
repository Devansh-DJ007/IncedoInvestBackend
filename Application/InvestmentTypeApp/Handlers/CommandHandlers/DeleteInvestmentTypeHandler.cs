using IncedoInvest.Application.InvestmentTypeApp.Commands;
using IncedoInvest.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace IncedoInvest.Application.InvestmentTypeApp.Handlers
{
    public class DeleteInvestmentTypeHandler : IRequestHandler<DeleteInvestmentTypeCommand, bool>
    {
        private readonly IInvestmentTypeRepository _repository;

        public DeleteInvestmentTypeHandler(IInvestmentTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteInvestmentTypeCommand request, CancellationToken cancellationToken)
        {
            var existingInvestmentType = await _repository.GetInvestmentTypeByIdAsync(request.InvestmentTypeId);

            if (existingInvestmentType == null)
            {
                return false;
            }

            await _repository.DeleteInvestmentTypeAsync(request.InvestmentTypeId);

            return true;
        }
    }
}
