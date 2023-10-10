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
    }
}
