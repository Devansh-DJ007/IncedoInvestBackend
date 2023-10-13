using IncedoInvest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncedoInvest.Domain.Interfaces
{
    public interface IInvestmentStrategyRepository
    {
        public Task AddStrategyAsync(InvestmentStrategy investmentStrategy);
        public Task UpdateStrategyAsync(InvestmentStrategy investmentStrategy);
        public Task DeleteStrategyAsync(int id);
        public Task<List<InvestmentStrategy>> GetAllStrategyAsync();
        public Task<InvestmentStrategy> GetStrategyByIdAsync(int id);
        public Task<InvestmentStrategy> GetStrategyByNameAsync(string strategyName);
        public double GetReturn1YByInvestmentStrategyId(int investmentStrategyId);
        public double GetReturn10YByInvestmentStrategyId(int investmentStrategyId);
        public double GetRisk10YByInvestmentStrategyId(int investmentStrategyId);
    }
}
