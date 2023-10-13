using IncedoInvest.Application.ProposedInvestmentApp.Commands;
using IncedoInvest.Application.Services;
using IncedoInvest.Domain.Entities;
using IncedoInvest.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncedoInvest.Application.ProposedInvestmentApp.Handlers.CommandHandlers
{
    public class CreateProposedInvestmentHandler : IRequestHandler<CreateProposedInvestmentCommand, Result<string>>
    {
        private readonly IProposedInvestmentRepository _repository;

        public CreateProposedInvestmentHandler(IProposedInvestmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<string>> Handle(CreateProposedInvestmentCommand request, CancellationToken cancellationToken)
        {
            var proposedInvestment = new ProposedInvestment
            {
                InvestmentInfoId = request.InvestmentInfoId,
                InvestmentStrategyId = request.InvestmentStrategyId,
                AcceptedFlag = false,
            };

            await _repository.AddProposedInvestmentAsync(proposedInvestment);

            return Result<string>.Success("PropsedInvestment Added successfully");
        }
    }
}
