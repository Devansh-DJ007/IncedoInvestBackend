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
        public Task<Users> GetUserByIdAsync(int id);
        public Task<Users> GetUserByEmailAsync(string email);
        public Task AddUserAsync(Users user);
        public Task UpdateUserAsync(Users user);
        public Task DeleteUserAsync(int id);
        public Task<List<Users>> GetUsersAsync();
        public Task<List<Users>> GetUsersByRoleIdAsync(int roleId);
    }
}
