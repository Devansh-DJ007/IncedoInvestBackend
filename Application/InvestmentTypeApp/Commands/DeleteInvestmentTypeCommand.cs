using MediatR;

namespace IncedoInvest.Application.InvestmentTypeApp.Commands
{
    public class DeleteInvestmentTypeCommand : IRequest<bool>
    {
        public int InvestmentTypeId { get; set; }
    }
}
