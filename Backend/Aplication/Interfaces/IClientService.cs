using CRM.Aplication.Request;
using CRM.Aplication.Response;

namespace CRM.Aplication.Interfaces
{
    public interface IClientService
    {
        public Task<ClientResponse> RegisterClient(CreateClientRequest client);
        public Task<List<ClientResponse>> GetClients();

    }
}
