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

            var advisorDashboardDTOs = new List<AdvisorDashboardDTO>();

            foreach (var client in clients)
            {
                var clientName = $"{client.FirstName} {client.LastName}";
                var advisorId = request.AdvisorId;
                var investmentAmount = await _investmentInfoRepository.GetTotalInvestmentAmountForClientAsync(client.UserId).ConfigureAwait(false);
                var investementType = await _investmentInfoRepository.GetInvestmentTypeAsync(client.UserId).ConfigureAwait(false);

                var advisorDashboardDTO = new AdvisorDashboardDTO
                {
                    ClientName = clientName,
                    AdvisorId = advisorId,
                    InvestmentAmount = investmentAmount,
                    InvestementType = investementType,
                    Status = "Funded"
                };

                advisorDashboardDTOs.Add(advisorDashboardDTO);
            }

            return advisorDashboardDTOs;
        }
    }
}
