using IncedoInvest.Domain.Entities;
using IncedoInvest.Domain.Interfaces;
using IncedoInvest.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncedoInvest.Infrastructure.Repositories
{
    public class InvestmentInfoRepository : IInvestmentInfoRepository
    {
        private readonly AppDbContextClass _dbContext;

        public InvestmentInfoRepository(AppDbContextClass dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task AddInvestmentInfoAsync(InvestmentInfo investmentInfo)
        {
            if (investmentInfo == null)
            {
                throw new ArgumentNullException(nameof(investmentInfo));
            }

            try
            {
                _dbContext.InvestmentInfos.Add(investmentInfo);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException;
                if (innerException != null)
                {
                    Console.WriteLine($"Inner Exception: {innerException.Message}");
                }
                throw;
            }
        }

        public async Task UpdateInvestmentInfoAsync(InvestmentInfo investmentInfo)
        {
            if (investmentInfo == null)
            {
                throw new ArgumentNullException(nameof(investmentInfo));
            }

            try
            {
                _dbContext.InvestmentInfos.Update(investmentInfo);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException;
                throw;
            }
        }

        public async Task DeleteInvestmentInfoAsync(int id)
        {
            var investmentInfoToDelete = await _dbContext.InvestmentInfos.FindAsync(id);
            if (investmentInfoToDelete != null)
            {
                try
                {
                    _dbContext.InvestmentInfos.Remove(investmentInfoToDelete);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    var innerException = ex.InnerException;
                    throw;
                }
            }
        }

        public async Task<List<InvestmentInfo>> GetAllInvestmentInfoAsync()
        {
            return await _dbContext.InvestmentInfos.ToListAsync();
        }

        public async Task<InvestmentInfo> GetInvestmentInfoByIdAsync(int id)
        {
            return await _dbContext.InvestmentInfos
                .Where(i => i.InvestmentInfoId == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<InvestmentInfo>> GetInvestmentInfoByClientIdAsync(string clientId)
        {
            int userId = await GetUserIdByClientIdAsync(clientId);

            if (userId > 0)
            {
                var investmentInfos = await _dbContext.InvestmentInfos
                    .Where(ii => ii.UserId == userId)
                    .ToListAsync();

                return investmentInfos;
            }

            return null;

        }

        private async Task<int> GetUserIdByClientIdAsync(string clientId)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.ClientId == clientId);

            if (user != null)
            {
                return user.UserId;
            }

            return -1;
        }

        public async Task<bool> InvestmentInfoExistsAsync(int id)
        {
            return await _dbContext.InvestmentInfos
                .AnyAsync(i => i.InvestmentInfoId == id);
        }

        public async Task<double> GetTotalInvestmentAmountForClientAsync(int userId)
        {
            var investments = await _dbContext.InvestmentInfos.Where(user => user.UserId == userId).ToListAsync();

            double totalInvestmentAmount = investments.Sum(investment => investment.InvestmentAmount);

            return totalInvestmentAmount;
        }
    }
}

