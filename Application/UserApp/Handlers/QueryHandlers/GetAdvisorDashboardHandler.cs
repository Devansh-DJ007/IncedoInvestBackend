using IncedoInvest.Application.Services;
using IncedoInvest.Application.UserApp.Queries;
using IncedoInvest.Domain.Interfaces;
using MediatR;

namespace IncedoInvest.Application.UserApp.Handlers.QueryHandlers
{
    public class GetAdvisorDashboardHandler : IRequestHandler<GetAdvisorDashboardQuery, List<AdvisorDashboardDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IInvestmentInfoRepository _investmentInfoRepository;

        public GetAdvisorDashboardHandler(IUserRepository userRepository, IInvestmentInfoRepository investmentInfoRepository)
        {
            _userRepository = userRepository;
            _investmentInfoRepository = investmentInfoRepository;
        }

        public async Task<List<AdvisorDashboardDTO>> Handle(GetAdvisorDashboardQuery request, CancellationToken cancellationToken)
        {
            var clients = await _userRepository.GetClientsByAdvisorIdAsync(request.AdvisorId);

            var advisorDashboardTasks = clients.Select(async client => new AdvisorDashboardDTO
            {
                ClientName = $"{client.FirstName} {client.LastName}",
                AdvisorId = request.AdvisorId,
                InvestmentAmount = await _investmentInfoRepository.GetTotalInvestmentAmountForClientAsync(client.UserId).ConfigureAwait(false)
            });

            var advisorDashboardDTOs = await Task.WhenAll(advisorDashboardTasks);

            return advisorDashboardDTOs.ToList();
        }
    }
}
