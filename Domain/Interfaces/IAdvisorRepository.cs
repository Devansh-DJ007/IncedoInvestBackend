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
        public Task AddAdvisorAsync(Advisor advisor);
        public Task UpdateAdvisorAsync(Advisor advisor);
        public Task DeleteAdvisorAsync(int id);
        public Task<List<Advisor>> GetAllAdvisorAsync();
        public Task<Advisor> GetAdvisorByIdAsync(int id);
        public Task<Advisor> GetAdvisorByEmailAsync(string email);
        public Task<bool> AdvisorExistsAsync(string email);
    }
}
