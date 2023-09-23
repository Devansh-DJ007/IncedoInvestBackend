using IncedoInvest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncedoInvest.Domain.Interfaces
{
    public interface IAdvisorRepository
    {
        public Task<bool> AdvisorExistsAsync(string username);
        public Task<AdvisorDetails> GetAdvisorByIdAsync(int id);
        public Task<AdvisorDetails> GetAdvisorByEmailAsync(string username);
        public Task AddAdvisorAsync(AdvisorDetails advisor);
        public Task UpdateAdvisorAsync(AdvisorDetails advisor);
        public Task DeleteAdvisorAsync(int id);
    }
}
