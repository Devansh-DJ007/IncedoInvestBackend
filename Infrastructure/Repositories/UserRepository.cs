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

        public async Task<User> GetUserByIdAsync(int Id)
        {
            try
            {
                return await _dbContext.Users.Where(x => x.UserId == Id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            try
            {
                return await _dbContext.Users.SingleOrDefaultAsync(a => a.Email == email);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task AddUserAsync(User user)
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

        public async Task UpdateUserAsync(User user)
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
                var innerException = ex.InnerException;
                throw;
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
                    var innerException = ex.InnerException;
                    throw;
                }
            }
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            return await _dbContext.Users.AnyAsync(a => a.Email == email);   
        }
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }
        public async Task<List<User>> GetUsersByRoleIdAsync(int roleId)
        {
            return await _dbContext.Users.Where(u => u.RoleId == roleId).ToListAsync();
        }

        public async Task<bool> CheckUserExistsWithAdvisorIdAsync(string advisorId)
        {
            bool userExists = await _dbContext.Users.AnyAsync(a => a.AdvisorId == advisorId);

            return userExists;
        }

        public async Task<bool> CheckUserExistsWithClientIdAsync(string clientId)
        {
            bool userExists = await _dbContext.Users.AnyAsync(a => a.ClientId == clientId);

            return userExists;
        }

        public async Task<List<User>> GetClientsByAdvisorIdAsync(string advisorId)
        {
            var clients = await _dbContext.Users
                .Where(user => user.AdvisorId == advisorId && user.RoleId == 2)
                .ToListAsync();

            return clients;
        }
    }
}
