using CRM.Aplication.Interfaces;
using CRM.Aplication.Mapper.IMappers;
using CRM.Aplication.Request;
using CRM.Aplication.Response;
using CRM.Domain.Entities;
using System.Text.RegularExpressions;

namespace CRM.Aplication.UseCases
{
    public class ClientsService : IClientService
    {
        private readonly IClientsCommand _command;
        private readonly IClientsQuery _query;
        private readonly IClientMapper _mapper;

        public ClientsService(IClientsCommand command, IClientsQuery query, IClientMapper mapper)
        {
            _command = command;
            _query = query;
            _mapper = mapper;
        }

        public async Task<ClientResponse> RegisterClient(CreateClientRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request), "Request cannot be null.");

            if (string.IsNullOrWhiteSpace(request.name))
                throw new ArgumentException("Clients name cannot be empty.");

            if (string.IsNullOrWhiteSpace(request.email))
                throw new ArgumentException("Clients email cannot be empty.");

            if (string.IsNullOrWhiteSpace(request.company))
                throw new ArgumentException("Clients company cannot be empty.");

            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!emailRegex.IsMatch(request.email))
                throw new ArgumentException("Invalid email format.");

            if (!string.IsNullOrWhiteSpace(request.phone) && request.phone.Length < 10)
                throw new ArgumentException("Phone number must contain at least 10 digits.");


            var cliente = new Clients
            {
                Name = request.name,
                Email = request.email,
                Company = request.company,
                Phone = request.phone,
                Address = request.address,
                CreateDate = DateTime.Now
            };

            var clientee = await _command.AddClient(cliente);
            return await _mapper.GetClientResponseMapper(clientee);
        }

        public async Task<List<ClientResponse>> GetClients()
        {
            var clientes = await _query.GetAllClients();
            return await _mapper.GetAllClientResponseMapper(clientes);
        }
    }
}
