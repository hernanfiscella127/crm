using CRM.Aplication.Mapper.IMappers;
using CRM.Aplication.Response;
using CRM.Domain.Entities;

namespace CRM.Aplication.Mapper
{
    public class ClientMapper : IClientMapper
    {
        public async Task<List<ClientResponse>> GetAllClientResponseMapper(List<Clients> allClients)
        {
            List<ClientResponse> list = new List<ClientResponse>();
            foreach (var client in allClients)
            {
                var response = new ClientResponse
                {
                    id = client.ClientID,
                    address = client.Address,
                    company = client.Company,
                    email = client.Email,
                    name = client.Name,
                    phone = client.Phone
                };
                list.Add(response);
            }
            return await System.Threading.Tasks.Task.FromResult(list);
        }

        public async Task<ClientResponse> GetClientResponseMapper(Clients client)
        {
            var response = new ClientResponse
            {
                id = client.ClientID,
                address = client.Address,
                company = client.Company,
                email = client.Email,
                name = client.Name,
                phone = client.Phone

            };
            return await System.Threading.Tasks.Task.FromResult(response);
        }
    }
}
