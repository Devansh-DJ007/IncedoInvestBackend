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
    public class CreateInvestmentInfoCommand : IRequest<Result<string>>
    {
        public int UserId { get; set; }
        public decimal InvestmentAmount { get; set; }
        public int InvestmentTypeId { get; set; }
    }
}
