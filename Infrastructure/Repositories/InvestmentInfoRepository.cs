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
                _dbContext.InvestorInfos.Add(investmentInfo);
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
                _dbContext.InvestorInfos.Update(investmentInfo);
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
            var investmentInfoToDelete = await _dbContext.InvestorInfos.FindAsync(id);
            if (investmentInfoToDelete != null)
            {
                try
                {
                    _dbContext.InvestorInfos.Remove(investmentInfoToDelete);
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
            return await _dbContext.InvestorInfos.ToListAsync();
        }

        public async Task<InvestmentInfo> GetInvestmentInfoByIdAsync(int id)
        {
            return await _dbContext.InvestorInfos
                .Where(i => i.InvestorInfoId == id)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> InvestmentInfoExistsAsync(int id)
        {
            return await _dbContext.InvestorInfos
                .AnyAsync(i => i.InvestorInfoId == id);
        }

        public Task<List<InvestmentInfo>> GetInvestmentInfoByInvestmentTypeAsync(int investmentTypeId)
        {
            throw new NotImplementedException();
        }

        //public async Task<List<InvestmentInfo>> GetInvestmentInfoByClientIdAsync(int clientId)
        //{
        //    return await _dbContext.InvestorInfos
        //        .Where(i => i.ClientId == clientId)
        //        .ToListAsync();
        //}

        //public async Task<List<InvestmentInfo>> GetInvestmentInfoByAdvisorIdAsync(int advisorId)
        //{
        //    return await _dbContext.InvestorInfos
        //        .Where(i => i.AdvisorId == advisorId)
        //        .ToListAsync();
        //}

        //public async Task<List<InvestmentInfo>> GetInvestmentInfoByInvestmentTypeAsync(int investmentTypeId)
        //{
        //    return await _dbContext.InvestorInfos
        //        .Where(i => i.InvestmentTypeId == investmentTypeId)
        //        .ToListAsync();
        //}
    }
}

