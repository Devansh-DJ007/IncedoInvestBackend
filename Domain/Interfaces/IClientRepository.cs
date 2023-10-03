using IncedoInvest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncedoInvest.Domain.Interfaces
{
    public interface IClientRepository
    {
        public Task AddClientAsync(Client client);
        public Task UpdateClientAsync(Client client);
        public Task DeleteClientAsync(int id);
        public Task<List<Client>> GetAllClientAsync();
        public Task<Client> GetClientByIdAsync(int id);
        public Task<Client> GetClientByEmailAsync(string email);
        public Task<bool> ClientExistsAsync(string email);
    }
}
