using MediatR;

namespace IncedoInvest.Application.UserApp.Queries
{
    public class GetInvestmentAmountForClientQuery :IRequest<double>
    {
        public int UserId { get; set; }
        public GetInvestmentAmountForClientQuery(int userId)
        {
            UserId = userId;
        }
    }
}
