using IncedoInvest.Application.InvestmentInfoApp.Queries;
using IncedoInvest.Application.Services;
using IncedoInvest.Domain.Entities;
using IncedoInvest.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IncedoInvest.Application.InvestmentInfoApp.Handlers
{
    public class GetAllInvestmentInfoHandler : IRequestHandler<GetAllInvestmentInfoQuery, Result<List<InvestmentInfo>>>
    {
        private readonly IInvestmentInfoRepository _investmentInfoRepository;

        public GetAllInvestmentInfoHandler(IInvestmentInfoRepository investmentInfoRepository)
        {
            _investmentInfoRepository = investmentInfoRepository;
        }

        public async Task<Result<List<InvestmentInfo>>> Handle(GetAllInvestmentInfoQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Implement the logic to retrieve all InvestmentInfo entities
                var investmentInfos = await _investmentInfoRepository.GetAllInvestmentInfoAsync();

                return Result<List<InvestmentInfo>>.Success(investmentInfos);
            }
            catch (Exception ex)
            {
                return Result<List<InvestmentInfo>>.Fail($"Error retrieving InvestmentInfo: {ex.Message}");
            }
        }
    }
}
