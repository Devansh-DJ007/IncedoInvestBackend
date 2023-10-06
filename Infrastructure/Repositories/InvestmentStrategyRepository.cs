
using IncedoInvest.Domain.Entities;
using IncedoInvest.Domain.Interfaces;
using IncedoInvest.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace IncedoInvest.Infrastructure.Repositories
{
    public class InvestmentStrategyRepository : IInvestmentStrategyRepository
    {
        private readonly AppDbContextClass _dbContext;

        public InvestmentStrategyRepository(AppDbContextClass dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddStrategyAsync(InvestmentStrategy investmentStrategy)
        {
            if (investmentStrategy == null)
            {
                throw new ArgumentNullException(nameof(investmentStrategy));
            }

            try
            {
                _dbContext.InvestmentStrategies.Add(investmentStrategy);
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

        public async Task UpdateStrategyAsync(InvestmentStrategy investmentStrategy)
        {
            if (investmentStrategy== null)
            {
                throw new ArgumentNullException(nameof(investmentStrategy));
            }

            try
            {
                _dbContext.InvestmentStrategies.Update(investmentStrategy);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException;
                throw;
            }
        }

        public async Task DeleteStrategyAsync(int id)
        {
            var strategyToDelete = await _dbContext.InvestmentStrategies.FindAsync(id);
            if (strategyToDelete != null)
            {
                try
                {
                    _dbContext.InvestmentStrategies.Remove(strategyToDelete);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    var innerException = ex.InnerException;
                    throw;
                }
            }
        }

        public async Task<List<InvestmentStrategy>> GetAllStrategyAsync()
        {
            return await _dbContext.InvestmentStrategies.ToListAsync();
        }

        public async Task<InvestmentStrategy> GetStrategyByIdAsync(int Id)
        {
            return await _dbContext.InvestmentStrategies.Where(x => x.InvestmentStrategyId == Id).FirstOrDefaultAsync();
        }

        Task<Advisor> IInvestmentStrategyRepository.GetStrategyByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}