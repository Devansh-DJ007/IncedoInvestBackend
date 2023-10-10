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
    public class CreateInvestmentInfoHandler : IRequestHandler<CreateInvestmentInfoCommand, Result<string>>
    {
        private readonly IInvestmentInfoRepository _investmentInfoRepository;

        public CreateInvestmentInfoHandler(IInvestmentInfoRepository investmentInfoRepository)
        {
            _investmentInfoRepository = investmentInfoRepository;
        }

        public async Task<Result<string>> Handle(CreateInvestmentInfoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var investmentInfo = new InvestmentInfo
                {
                    UserId = request.UserId,
                    InvestmentAmount = (double)request.InvestmentAmount,
                    InvestmentTypeId = request.InvestmentTypeId,
                    Accepted = false,
                    DeletedFlag = false,
                };

                await _investmentInfoRepository.AddInvestmentInfoAsync(investmentInfo);

                return Result<string>.Success("InvestmentInfo created successfully");
            }
            catch (Exception ex)
            {
                return Result<string>.Fail($"Error creating InvestmentInfo: {ex}");
            }
        }
    }
}

