using IncedoInvest.Application.Services;
using IncedoInvest.Application.UserApp.Queries;
using IncedoInvest.Domain.Interfaces;
using MediatR;

namespace IncedoInvest.Application.UserApp.Handlers.QueryHandlers
{
    public class GetClientDashboardHandler : IRequestHandler<GetClientDashboardQuery, List<ClientDashboardDTO>>
    {
        private readonly IInvestmentInfoRepository _investmentInfoRepository;
        private readonly IInvestmentStrategyRepository _investmentStrategyRepository;
        private readonly IProposedInvestmentRepository _proposedInvestmentRepository;

        public GetClientDashboardHandler(IInvestmentInfoRepository investmentInfoRepository, IInvestmentStrategyRepository investmentStrategyRepository, IProposedInvestmentRepository proposedInvestmentRepository)
        {
            _investmentInfoRepository = investmentInfoRepository;
            _investmentStrategyRepository = investmentStrategyRepository;
            _proposedInvestmentRepository = proposedInvestmentRepository;   
        }

        public async Task<List<ClientDashboardDTO>> Handle(GetClientDashboardQuery request, CancellationToken cancellationToken)
        {
            var investmentInfos = await _investmentInfoRepository.GetInvestmentInfoByClientIdAsync(request.ClientId);

            var ClientDashboardDTOs = new List<ClientDashboardDTO>();

            foreach (var investmentInfo in investmentInfos)
            {
                var proposedInvestments = await _proposedInvestmentRepository.GetProposedInvestmentsByInvestmentInfoIdAsync(investmentInfo.InvestmentInfoId);

                if (investmentInfo != null && proposedInvestments != null)
                {
                    var clientInvestmentTasks = proposedInvestments
                        .Where(pi => pi != null)
                        .Select(async pi =>
                        {
                            var clientInvestmentDTO = new ClientDashboardDTO
                            {
                                proposedInvestmentId = pi.PropesedInvestmentId,
                                investmentInfoId = pi.InvestmentInfoId,
                                investmentAmount = investmentInfo.InvestmentAmount,
                                return1Y = _investmentStrategyRepository.GetReturn1YByInvestmentStrategyId(pi.InvestmentStrategyId),
                                return10Y = _investmentStrategyRepository.GetReturn10YByInvestmentStrategyId(pi.InvestmentStrategyId),
                                risk10Y = _investmentStrategyRepository.GetRisk10YByInvestmentStrategyId(pi.InvestmentStrategyId),
                                acceptedFlag = pi.AcceptedFlag,
                            };

                            clientInvestmentDTO.return1Y = clientInvestmentDTO.investmentAmount + clientInvestmentDTO.investmentAmount * (clientInvestmentDTO.return1Y/100);
                            clientInvestmentDTO.return10Y = clientInvestmentDTO.investmentAmount + clientInvestmentDTO.investmentAmount * (clientInvestmentDTO.return10Y/100);
                            clientInvestmentDTO.risk10Y = clientInvestmentDTO.investmentAmount - clientInvestmentDTO.investmentAmount * (clientInvestmentDTO.risk10Y/100);

                            return clientInvestmentDTO;
                        });

                    var clientInvestmentDTOs = await Task.WhenAll(clientInvestmentTasks);
                    ClientDashboardDTOs.AddRange(clientInvestmentDTOs);
                }
            }
            return ClientDashboardDTOs;
        }
    }
}
