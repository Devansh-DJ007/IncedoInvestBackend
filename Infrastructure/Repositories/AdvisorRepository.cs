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
    public class AdvisorRepository : IAdvisorRepository
    {
        private readonly AdvisorDbContextClass _dbContext;

        public AdvisorRepository(AdvisorDbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AdvisorDetails> GetAdvisorByIdAsync(int Id)
        {
            try
            {
                return await _dbContext.Advisors.Where(x => x.UserID == Id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // Log or handle the exception here
                throw; // Rethrow the exception to propagate it up the call stack
            }
        }

        public async Task<AdvisorDetails> GetAdvisorByEmailAsync(string email)
        {
            try
            {
                return await _dbContext.Advisors.SingleOrDefaultAsync(a => a.Email == email);
            }
            catch (Exception ex)
            {
                // Log or handle the exception here
                throw; // Rethrow the exception to propagate it up the call stack
            }
        }

        public async Task AddAdvisorAsync(AdvisorDetails advisor)
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
                // Log the inner exception for detailed error information
                var innerException = ex.InnerException;
                // Log or inspect innerException to see the specific error message
                if (innerException != null)
                {
                    // Log or inspect the innerException to see the specific error message
                    Console.WriteLine($"Inner Exception: {innerException.Message}");
                }
                throw; // Rethrow the exception to propagate it up the call stack
            }
        }

        public async Task UpdateAdvisorAsync(AdvisorDetails advisor)
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
                // Log the inner exception for detailed error information
                var innerException = ex.InnerException;
                // Log or inspect innerException to see the specific error message
                throw; // Rethrow the exception to propagate it up the call stack
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
                    // Log the inner exception for detailed error information
                    var innerException = ex.InnerException;
                    // Log or inspect innerException to see the specific error message
                    throw; // Rethrow the exception to propagate it up the call stack
                }
            }
        }

        public async Task<bool> AdvisorExistsAsync(string email)
        {
            try
            {
                return await _dbContext.Advisors.AnyAsync(a => a.Email == email);
            }
            catch (Exception ex)
            {
                // Log or handle the exception here
                throw; // Rethrow the exception to propagate it up the call stack
            }
        }
    }
}
