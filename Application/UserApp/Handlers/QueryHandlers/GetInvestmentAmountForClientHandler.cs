using IncedoInvest.Application.UserApp.Queries;
using IncedoInvest.Domain.Interfaces;
using MediatR;

namespace IncedoInvest.Application.UserApp.Handlers.QueryHandlers
{
    public class GetInvestmentAmountForClientHandler : IRequestHandler<GetInvestmentAmountForClientQuery, double>
    {
        private readonly IUserRepository _userRepository;
        private readonly IInvestmentInfoRepository _investmentInfoRepository;

        public GetInvestmentAmountForClientHandler(
            IUserRepository userRepository,
            IInvestmentInfoRepository investmentInfoRepository)
        {
            _userRepository = userRepository;
            _investmentInfoRepository = investmentInfoRepository;
        }

        public async Task<double> Handle(GetInvestmentAmountForClientQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(request.UserId);

            var investmentAmount = await _investmentInfoRepository.GetTotalInvestmentAmountForClientAsync(request.UserId);

            return investmentAmount;
        }
    }
}
