using IncedoInvest.Domain.Entities;
using MediatR;

namespace IncedoInvest.Application.InvestmentTypeApp.Queries
{
    public class GetInvestmentTypeByIdQuery : IRequest<InvestmentType>
    {
        public int InvestmentTypeId { get; set; }
    }
}
