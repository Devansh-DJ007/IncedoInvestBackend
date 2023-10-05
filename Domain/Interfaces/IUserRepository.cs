using IncedoInvest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncedoInvest.Domain.Interfaces
{
    public interface IUserRepository
    {
        public Task<bool> UserExistsAsync(string email);
        public Task<User> GetUserByIdAsync(int id);
        public Task<User> GetUserByEmailAsync(string email);
        public Task AddUserAsync(User user);
        public Task UpdateUserAsync(User user);
        public Task DeleteUserAsync(int id);
        public Task<List<User>> GetAllUsersAsync();
        public Task<List<User>> GetUsersByRoleIdAsync(int roleId);
        public Task<bool> CheckUserExistsWithAdvisorIdAsync(string advisorId);
        public Task<bool> CheckUserExistsWithClientIdAsync(string clientId);
    }
}
