using IncedoInvest.Domain.Entities;
using IncedoInvest.Domain.Interfaces;
using IncedoInvest.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace IncedoInvest.Infrastructure.Repositories
{
    public class ProposedInvestmentRepository : IProposedInvestmentRepository
    {
        private readonly AppDbContextClass _dbContext;

        public ProposedInvestmentRepository(AppDbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddProposedInvestmentAsync(ProposedInvestment propesedInvestment)
        {
            _dbContext.Add(propesedInvestment);
            _dbContext.SaveChangesAsync();
        }

        public async Task<List<ProposedInvestment>> GetAllProposedInvestmentsAsync()
        {
            return await _dbContext.ProposedInvestments.ToListAsync();
        }

        public async Task<List<ProposedInvestment>> GetProposedInvestmentsByInvestmentInfoIdAsync(int investmentInfoId)
        {
            return await _dbContext.ProposedInvestments
                .Where(pi => pi.InvestmentInfoId == investmentInfoId)
                .ToListAsync();
        }

        public async Task AcceptProposedInvestmentAsync(int proposedInvestmentId)
        {
            var proposedInvestment = await _dbContext.ProposedInvestments.FindAsync(proposedInvestmentId);
            proposedInvestment.AcceptedFlag = true;
            _dbContext.ProposedInvestments.Update(proposedInvestment);
            var investmentInfo = await _dbContext.InvestmentInfos.FindAsync(proposedInvestment.InvestmentInfoId);
            investmentInfo.Accepted = true;


            var investmentinfos = await _dbContext.ProposedInvestments
                .Where(proposedinvestment => proposedinvestment.InvestmentInfoId == investmentInfo.InvestmentInfoId &&
                proposedinvestment.AcceptedFlag == false).ToListAsync();

            foreach (var proposed in investmentinfos)
            {
                _dbContext.ProposedInvestments.Remove(proposed);
            }

            _dbContext.InvestmentInfos.Update(investmentInfo);
            await _dbContext.SaveChangesAsync();
        }
    }
}
