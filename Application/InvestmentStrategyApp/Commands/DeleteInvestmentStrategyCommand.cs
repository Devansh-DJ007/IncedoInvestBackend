using IncedoInvest.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IncedoInvest.Application.InvestmentStrategyApp.Command
{
    public class DeleteInvestmentStrategyCommand : IRequest<Result<string>>
    {
        public int InvestmentId { get; set; } 
    }
}
