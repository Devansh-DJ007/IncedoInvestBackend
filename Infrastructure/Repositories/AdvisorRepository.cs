using IncedoInvest.Domain.Entities;
using IncedoInvest.Domain.Interfaces;
using IncedoInvest.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace IncedoInvest.Infrastructure.Repositories
{
    public class AdvisorRepository : IAdvisorRepository
    {
        private readonly AppDbContextClass _dbContext;

        public AdvisorRepository(AppDbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAdvisorAsync(Advisor advisor)
        {
            if (advisor == null)
            {
                throw new ArgumentNullException(nameof(advisor));
            }

            try
            {
                _dbContext.Advisors.Add(advisor);
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

        public async Task UpdateAdvisorAsync(Advisor advisor)
        {
            if (advisor == null)
            {
                throw new ArgumentNullException(nameof(advisor));
            }

            try
            {
                _dbContext.Advisors.Update(advisor);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException;
                throw;
            }
        }

        public async Task DeleteAdvisorAsync(int id)
        {
            var advisorToDelete = await _dbContext.Advisors.FindAsync(id);
            if (advisorToDelete != null)
            {
                try
                {
                    _dbContext.Advisors.Remove(advisorToDelete);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    var innerException = ex.InnerException;
                    throw;
                }
            }
        }

        public async Task<List<Advisor>> GetAllAdvisorAsync()
        {
            return await _dbContext.Advisors.ToListAsync();
        }

        public async Task<Advisor> GetAdvisorByIdAsync(int Id)
        {
            return await _dbContext.Advisors.Where(x => x.AdvisorId == Id).FirstOrDefaultAsync();
        }

        public async Task<Advisor> GetAdvisorByEmailAsync(string email)
        {
            return await _dbContext.Advisors.SingleOrDefaultAsync(a => a.Email == email);
        }

        public async Task<bool> AdvisorExistsAsync(string email)
        {
            return await _dbContext.Advisors.AnyAsync(a => a.Email == email);
        }
    }
}
