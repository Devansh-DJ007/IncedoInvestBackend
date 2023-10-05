using MediatR;

namespace IncedoInvest.Application.InvestmentTypeApp.Commands
{
    public class UpdateInvestmentTypeCommand : IRequest<bool>
    {
        public int InvestmentTypeId { get; set; }
        public string InvestmentTypeName { get; set; }
    }
}
