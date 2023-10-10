using IncedoInvest.Application.Services;
using MediatR;

namespace IncedoInvest.Application.UserApp.Queries
{
    public class GetAdvisorDashboardQuery : IRequest<List<AdvisorDashboardDTO>>
    {
        public string AdvisorId { get; set; }
    }
}
