using CRM.Domain.Entities;

namespace CRM.Aplication.Interfaces
{
    public interface IClientsCommand
    {
        public Task<Clients> AddClient(Clients cliente);

    }
}
