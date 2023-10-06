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
    public class UpdateInvestmentInfoCommand : IRequest<Result<string>>
    {
        public int InvestmentInfoId { get; set; }
        public int UserId { get; set; }
        //public int AdvisorId { get; set; }
        public decimal InvestmentAmount { get; set; }
        public int InvestmentTypeId { get; set; }
    }
}
