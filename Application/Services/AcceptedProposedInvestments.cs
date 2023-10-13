using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncedoInvest.Application.Services
{
    public class AcceptedProposedInvestments
    {
        public int proposedInvestmentId { get; set; }
        public int investmentInfoId { get; set; }
        public double investmentAmount { get; set; }
        public double return1Y { get; set; }
        public double return10Y { get; set; }
        public double risk10Y { get; set; }
        public bool acceptedFlag { get; set; }
    }
}
