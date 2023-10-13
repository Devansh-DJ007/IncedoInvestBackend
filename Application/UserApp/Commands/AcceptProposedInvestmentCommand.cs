using IncedoInvest.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncedoInvest.Application.UserApp.Commands
{
    public class AcceptProposedInvestmentCommand : IRequest<Result<string>>
    {
        public int proposedInvestmentId { get; set; }
    }
}
