using IncedoInvest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncedoInvest.Domain.Interfaces
{
    public interface IProposedInvestmentRepository
    {
        public Task AddProposedInvestmentAsync(ProposedInvestment propesedInvestment);
        public Task<List<ProposedInvestment>> GetAllProposedInvestmentsAsync();
        public Task<List<ProposedInvestment>> GetProposedInvestmentsByInvestmentInfoIdAsync(int investmentInfoId);
        public Task AcceptProposedInvestmentAsync(int proposedInvestmentId);
    }
}
