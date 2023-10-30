using IncedoInvest.Application.Services;
using IncedoInvest.Application.UserApp.Queries;
using IncedoInvest.Domain.Entities;
using IncedoInvest.Domain.Interfaces;
using MediatR;

namespace IncedoInvest.Application.UserApp.Handlers.QueryHandlers
{
    public class GetAdvisorDashboardHandler : IRequestHandler<GetAdvisorDashboardQuery, List<AdvisorDashboardDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IInvestmentInfoRepository _investmentInfoRepository;
        private readonly IProposedInvestmentRepository _proposedInvestmentRepository;

        public GetAdvisorDashboardHandler(IUserRepository userRepository, IInvestmentInfoRepository investmentInfoRepository, IProposedInvestmentRepository proposedInvestmentRepository)
        {
            _userRepository = userRepository;
            _investmentInfoRepository = investmentInfoRepository;
            _proposedInvestmentRepository = proposedInvestmentRepository;
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
                var status = "Invested";

                var proposedInvestments = new List<CustomProposedInvestmentDTO>();

                var investmentInfos = await _investmentInfoRepository.GetInvestmentInfoByClientIdAsync(client.ClientId);

                foreach (var investmentInfo in investmentInfos)
                {
                    var investments = await _proposedInvestmentRepository.GetProposedInvestmentsByInvestmentInfoIdAsync(investmentInfo.InvestmentInfoId);
                    proposedInvestments.AddRange(investments
                        .Where(pi => !pi.AcceptedFlag)
                        .Select(pi => new CustomProposedInvestmentDTO
                        {
                            ProposedInvestmentId = pi.PropesedInvestmentId,
                            InvestmentAmount = pi.InvestmentInfo.InvestmentAmount,
                            InvestmentType = _investmentInfoRepository.GetInvestmentTypeByInvestmentTypeId(pi.InvestmentInfo.InvestmentTypeId),
                        }));
                }
                    var advisorDashboardDTO = new AdvisorDashboardDTO
                {
                    ClientName = clientName,
                    AdvisorId = advisorId,
                    InvestmentAmount = investmentAmount,
                    InvestementType = investementType,
                    Status = status,
                    ProposedInvestments = proposedInvestments,
                };

                advisorDashboardDTOs.Add(advisorDashboardDTO);
            }

            return advisorDashboardDTOs;
        }
    }
}
