using CRM.Aplication.Exceptions;
using CRM.Aplication.Interfaces;
using CRM.Domain.Entities;
using CRM.Infraestructure.Percistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infraestructure.Command
{
    public class ClientsCommand : IClientsCommand
    {
        private readonly CrmContext _context;
        public ClientsCommand(CrmContext context)
        {
            _context = context;
        }
        public async Task<Clients> AddClient(Clients cliente)
        {
            try
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return cliente;
            }
            catch (DbUpdateException ex)
            {
                throw new ConflictException("There was an issue adding the client tho the database.", ex);
            }

        }
    }
}
