using IncedoInvest.Application.Services;
using MediatR;

namespace IncedoInvest.Application.InvestmentTypeApp.Commands
{
    public class CreateInvestmentTypeCommand : IRequest<Result<string>>
    {
        public string InvestmentTypeName { get; set; }
    }
}
