using IncedoInvest.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IncedoInvest.Application.InvestmentInfoApp.Commands
{
    public class DeleteInvestmentInfoCommand : IRequest<Result<string>>
    {
        public int InvestmentInfoId { get; set; }
    }
}
