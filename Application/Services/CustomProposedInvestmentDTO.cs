using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncedoInvest.Application.Services
{
    public class CustomProposedInvestmentDTO
    {
        public int ProposedInvestmentId { get; set; }
        public double InvestmentAmount { get; set; }
        public string InvestmentType { get; set; }

    }
}
