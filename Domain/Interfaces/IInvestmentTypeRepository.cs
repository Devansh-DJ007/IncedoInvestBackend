using IncedoInvest.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IncedoInvest.Domain.Interfaces
{
    public interface IInvestmentTypeRepository
    {
        Task<InvestmentType> GetInvestmentTypeByIdAsync(int id);
        Task<List<InvestmentType>> GetAllInvestmentTypesAsync();
        Task AddInvestmentTypeAsync(InvestmentType investmentType);
        Task UpdateInvestmentTypeAsync(InvestmentType investmentType);
        Task DeleteInvestmentTypeAsync(int id);
    }
}
