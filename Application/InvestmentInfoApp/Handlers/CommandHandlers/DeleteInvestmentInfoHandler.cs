using IncedoInvest.Application.InvestmentInfoApp.Commands;
using IncedoInvest.Application.Services;
using IncedoInvest.Domain.Entities;
using IncedoInvest.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IncedoInvest.Application.InvestmentInfoApp.Handlers
{
    public class DeleteInvestmentInfoHandler : IRequestHandler<DeleteInvestmentInfoCommand, Result<string>>
    {
        private readonly IInvestmentInfoRepository _investmentInfoRepository;

        public DeleteInvestmentInfoHandler(IInvestmentInfoRepository investmentInfoRepository)
        {
            _investmentInfoRepository = investmentInfoRepository;
        }

        public async Task<Result<string>> Handle(DeleteInvestmentInfoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Implement the logic to delete InvestmentInfo
                var investmentInfo = await _investmentInfoRepository.GetInvestmentInfoByIdAsync(request.InvestmentInfoId);

                if (investmentInfo == null)
                {
                    return Result<string>.Fail("InvestmentInfo not found");
                }

                await _investmentInfoRepository.DeleteInvestmentInfoAsync(investmentInfo.InvestorInfoId);

                return Result<string>.Success("InvestmentInfo deleted successfully");
            }
            catch (Exception ex)
            {
                return Result<string>.Fail($"Error deleting InvestmentInfo: {ex.Message}");
            }
        }
    }
}
