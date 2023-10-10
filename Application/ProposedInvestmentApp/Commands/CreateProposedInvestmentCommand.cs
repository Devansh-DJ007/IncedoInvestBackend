using IncedoInvest.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncedoInvest.Application.ProposedInvestmentApp.Commands
{
    public class CreateProposedInvestmentCommand : IRequest<Result<string>>
    {
        [Required(ErrorMessage = "InvestmentInfoId is required")]
        public int InvestmentInfoId { get; set; }

        [Required(ErrorMessage = "InvestmentStrategyId is required")]
        public int InvestmentStrategyId { get; set; }
    }
}
