using IncedoInvest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

  

namespace IncedoInvest.Domain.Interfaces
    {
        public interface IInvestmentInfoRepository
        {
            public Task AddInvestmentInfoAsync(InvestmentInfo investmentInfo);
            public Task UpdateInvestmentInfoAsync(InvestmentInfo investmentInfo);
            public Task DeleteInvestmentInfoAsync(int id);
            public Task<List<InvestmentInfo>> GetAllInvestmentInfoAsync();
            public Task<InvestmentInfo> GetInvestmentInfoByIdAsync(int id);
            public Task<bool> InvestmentInfoExistsAsync(int id);
            public Task<double> GetTotalInvestmentAmountForClientAsync(int userId);
            public Task<IEnumerable<InvestmentInfo>> GetInvestmentInfoByClientIdAsync(string clientId);
            public Task<string> GetInvestmentTypeAsync(int userId);
        }
    }

