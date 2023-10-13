using IncedoInvest.Domain.Entities;
using IncedoInvest.Domain.Interfaces;
using IncedoInvest.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            try
            {
                var proposedInvestment = await _dbContext.ProposedInvestments.FindAsync(proposedInvestmentId);
                proposedInvestment.AcceptedFlag = true;
                _dbContext.ProposedInvestments.Update(proposedInvestment);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException;
                throw innerException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
