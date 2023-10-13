using IncedoInvest.Application.Services;
using IncedoInvest.Application.UserApp.Commands;
using IncedoInvest.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncedoInvest.Application.UserApp.Handlers.CommandHandlers
{
    public class AcceptProposedInvestmentHandler : IRequestHandler<AcceptProposedInvestmentCommand, Result<string>>
    {
        private readonly IProposedInvestmentRepository _proposedInvestmentRepository;

        public AcceptProposedInvestmentHandler(IProposedInvestmentRepository proposedInvestmentRepository)
        {
            _proposedInvestmentRepository = proposedInvestmentRepository;
        }

        public async Task<Result<string>> Handle(AcceptProposedInvestmentCommand request, CancellationToken cancellationToken)
        {
            _proposedInvestmentRepository.AcceptProposedInvestmentAsync(request.proposedInvestmentId);
            return Result<string>.Success("Proposed Investment accepted successfully");
        }
    }
}
