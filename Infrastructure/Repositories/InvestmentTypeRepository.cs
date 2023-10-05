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
    public class InvestmentTypeRepository : IInvestmentTypeRepository
    {
        private readonly AppDbContextClass _dbContext;

        public InvestmentTypeRepository(AppDbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<InvestmentType> GetInvestmentTypeByIdAsync(int id)
        {
            try
            {
                return await _dbContext.InvestmentTypes.SingleOrDefaultAsync(i => i.InvestmentTypeId == id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<InvestmentType>> GetAllInvestmentTypesAsync()
        {
            return await _dbContext.InvestmentTypes.ToListAsync();
        }

        public async Task AddInvestmentTypeAsync(InvestmentType investmentType)
        {
            if (investmentType == null)
            {
                throw new ArgumentNullException(nameof(investmentType));
            }

            try
            {
                // Do not set InvestmentTypeId here, as it's an identity column
                _dbContext.InvestmentTypes.Add(investmentType);
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

        public async Task UpdateInvestmentTypeAsync(InvestmentType investmentType)
        {
            if (investmentType == null)
            {
                throw new ArgumentNullException(nameof(investmentType));
            }

            try
            {
                // Do not set InvestmentTypeId here, as it's an identity column
                _dbContext.InvestmentTypes.Update(investmentType);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException;
                throw;
            }
        }

        public async Task DeleteInvestmentTypeAsync(int id)
        {
            var investmentTypeToDelete = await _dbContext.InvestmentTypes.FindAsync(id);
            if (investmentTypeToDelete != null)
            {
                try
                {
                    // Do not set InvestmentTypeId here, as it's an identity column
                    _dbContext.InvestmentTypes.Remove(investmentTypeToDelete);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    var innerException = ex.InnerException;
                    throw;
                }
            }
        }
    }
}
