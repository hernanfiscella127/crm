using CRM.Aplication.Exceptions;
using CRM.Aplication.Interfaces;
using CRM.Domain.Entities;
using CRM.Infraestructure.Percistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infraestructure.Query
{
    public class ClientsQuery : IClientsQuery
    {
        private readonly CrmContext _context;

        public ClientsQuery(CrmContext context)
        {
            _context = context;
        }

        public async Task<List<Clients>> GetAllClients()
        {
            try
            {
                List<Clients> clients = await _context.Clients
                    .Include(c => c.Projects)
                    .ToListAsync();

                return clients;
            }
            catch (DbUpdateException)
            {
                throw new ConflictException("Error en la base de datos: Problema al obtener los clientes.");
            }
        }

        public async Task<Clients?> GetClientById(int clientId)
        {
            try
            {
                return await _context.Clients
                    .FirstOrDefaultAsync(c => c.ClientID == clientId);
            }
            catch (Exception ex)
            {
                throw new ConflictException("Error retrieving the client by ID from the database.", ex);
            }
        }
    }
}
