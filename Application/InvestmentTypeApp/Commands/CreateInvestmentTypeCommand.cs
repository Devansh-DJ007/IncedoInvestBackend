using MediatR;

namespace IncedoInvest.Application.InvestmentTypeApp.Commands
{
    public class CreateInvestmentTypeCommand : IRequest<int>
    {
        public string InvestmentTypeName { get; set; }
    }
}
