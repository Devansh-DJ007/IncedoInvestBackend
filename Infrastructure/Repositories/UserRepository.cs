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
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContextClass _dbContext;

        public UserRepository(AppDbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Users> GetUserByIdAsync(int Id)
        {
            try
            {
                return await _dbContext.Users.Where(x => x.UserID == Id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // Log or handle the exception here
                throw; // Rethrow the exception to propagate it up the call stack
            }
        }

        public async Task<Users> GetUserByEmailAsync(string email)
        {
            try
            {
                return await _dbContext.Users.SingleOrDefaultAsync(a => a.Email == email);
            }
            catch (Exception ex)
            {
                // Log or handle the exception here
                throw; // Rethrow the exception to propagate it up the call stack
            }
        }

        public async Task AddUserAsync(Users user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            try
            {
                _dbContext.Users.Add(user);
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

        public async Task UpdateUserAsync(Users user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            try
            {
                _dbContext.Users.Update(user);
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

        public async Task DeleteUserAsync(int id)
        {
            var userToDelete = await _dbContext.Users.FindAsync(id);
            if (userToDelete != null)
            {
                try
                {
                    _dbContext.Users.Remove(userToDelete);
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

        public async Task<bool> UserExistsAsync(string email)
        {
            try
            {
                return await _dbContext.Users.AnyAsync(a => a.Email == email);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<Users>> GetUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }
        public async Task<List<Users>> GetUsersByRoleIdAsync(int roleId)
        {
            return await _dbContext.Users.Where(u => u.RoleID == roleId).ToListAsync();
        }
    }
}
