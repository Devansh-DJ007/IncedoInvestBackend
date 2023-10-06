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
            Task AddInvestmentInfoAsync(InvestmentInfo investmentInfo);
            Task UpdateInvestmentInfoAsync(InvestmentInfo investmentInfo);
            Task DeleteInvestmentInfoAsync(int id);
            Task<List<InvestmentInfo>> GetAllInvestmentInfoAsync();
            Task<InvestmentInfo> GetInvestmentInfoByIdAsync(int id);
            //Task<List<InvestmentInfo>> GetInvestmentInfoByClientIdAsync(int clientId);
            //Task<List<InvestmentInfo>> GetInvestmentInfoByAdvisorIdAsync(int advisorId);
            Task<List<InvestmentInfo>> GetInvestmentInfoByInvestmentTypeAsync(int investmentTypeId);

            Task<bool> InvestmentInfoExistsAsync(int id);

        }
    }

