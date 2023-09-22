using IncedoInvest.Domain.Entities;
using IncedoInvest.Domain.Interfaces;
using IncedoInvest.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncedoInvest.Infrastructure.Repositories
{
    public class AdvisorRepository: IAdvisorRepository
    {
        private readonly AdvisorDbContextClass _dbContext;

        public AdvisorRepository(AdvisorDbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AdvisorDetails> GetAdvisorByIdAsync(int Id)
        {
            return await _dbContext.Advisors.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<AdvisorDetails> GetAdvisorByUsernameAsync(string username)
        {
            return await _dbContext.Advisors.SingleOrDefaultAsync(a => a.Username == username);
        }

        public async Task AddAdvisorAsync(AdvisorDetails advisor)
        {
            if (advisor == null)
            {
                throw new ArgumentNullException(nameof(advisor));
            }

            _dbContext.Advisors.Add(advisor);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAdvisorAsync(AdvisorDetails advisor)
        {
            if (advisor == null)
            {
                throw new ArgumentNullException(nameof(advisor));
            }

            _dbContext.Advisors.Update(advisor);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAdvisorAsync(int id)
        {
            var advisorToDelete = await _dbContext.Advisors.FindAsync(id);
            if (advisorToDelete != null)
            {
                _dbContext.Advisors.Remove(advisorToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task<bool> AdvisorExistsAsync(string username)
        {
            return await _dbContext.Advisors.AnyAsync(a => a.Username == username);
        }
    }
}
