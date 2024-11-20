using CRM.Aplication.Response;
using CRM.Domain.Entities;

namespace CRM.Aplication.Mapper.IMappers
{
    public interface IClientMapper
    {
        Task<ClientResponse> GetClientResponseMapper(Clients client);
        Task<List<ClientResponse>> GetAllClientResponseMapper(List<Clients> allClients);
    }
}
