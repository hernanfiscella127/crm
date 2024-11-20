using CRM.Domain.Entities;

namespace CRM.Aplication.Interfaces
{
    public interface IClientsQuery
    {
        public Task<List<Clients>> GetAllClients();
        public Task<Clients?> GetClientById(int clientId);

    }
}
