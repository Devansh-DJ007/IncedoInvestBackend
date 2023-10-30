using IncedoInvest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncedoInvest.Application.Services
{
    public class AdvisorDashboardDTO
    {
        public string ClientName { get; set; }
        public string AdvisorId { get; set; }
        public double InvestmentAmount { get; set; }
        public string InvestementType { get; set; }
        public string Status { get; set; }
        public List<CustomProposedInvestmentDTO> ProposedInvestments { get; set; }
    }
}
