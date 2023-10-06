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
    public class UpdateInvestmentInfoHandler : IRequestHandler<UpdateInvestmentInfoCommand, Result<string>>
    {
        private readonly IInvestmentInfoRepository _investmentInfoRepository;

        public UpdateInvestmentInfoHandler(IInvestmentInfoRepository investmentInfoRepository)
        {
            _investmentInfoRepository = investmentInfoRepository;
        }

        public async Task<Result<string>> Handle(UpdateInvestmentInfoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Implement the logic to update an existing InvestmentInfo
                var investmentInfo = await _investmentInfoRepository.GetInvestmentInfoByIdAsync(request.InvestmentInfoId);

                if (investmentInfo == null)
                {
                    return Result<string>.Fail("InvestmentInfo not found");
                }

                investmentInfo.UserId = request.UserId;
              //  investmentInfo.AdvisorId = request.AdvisorId;
                investmentInfo.InvestmentAmount = (double)request.InvestmentAmount;
                investmentInfo.InvestmentTypeId = request.InvestmentTypeId;
                // Update other properties as needed

                await _investmentInfoRepository.UpdateInvestmentInfoAsync(investmentInfo);

                return Result<string>.Success("InvestmentInfo updated successfully");
            }
            catch (Exception ex)
            {
                return Result<string>.Fail($"Error updating InvestmentInfo: {ex.Message}");
            }
        }
    }
}

