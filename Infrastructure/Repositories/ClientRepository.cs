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
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContextClass _dbContext;

        public ClientRepository(AppDbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddClientAsync(Client client)
        {
            _dbContext.Clients.Add(client);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateClientAsync(Client client)
        {
            _dbContext.Clients.Update(client);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteClientAsync(int id)
        {
            var clientToDelete = await _dbContext.Clients.FindAsync(id);
            if (clientToDelete != null) 
            {
                _dbContext.Clients.Remove(clientToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Client>> GetAllClientAsync()
        {
            return await _dbContext.Clients.ToListAsync();
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            return await _dbContext.Clients.Where(x => x.ClientId == id).FirstOrDefaultAsync();
        }

        public async Task<Client> GetClientByEmailAsync(string email)
        {
            return await _dbContext.Clients.SingleOrDefaultAsync(c => c.Email == email);
        }

        public async Task<bool> ClientExistsAsync(string email)
        {
            return await _dbContext.Clients.AnyAsync(c => c.Email == email);
        }
    }
}
