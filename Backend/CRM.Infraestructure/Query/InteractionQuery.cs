using CRM.Aplication.Exceptions;
using CRM.Aplication.Interfaces;
using CRM.Domain.Entities;
using CRM.Infraestructure.Percistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infraestructure.Query
{
    public class InteractionQuery : IInteractionQuery
    {
        private readonly CrmContext _context;
        public InteractionQuery(CrmContext context)
        {
            _context = context;
        }


        public async Task<Interactions> GetInteractionById(Guid id)
        {
            try
            {
                return await _context.Interactions
                .Include(i => i.InteractionTypes)
                .FirstOrDefaultAsync(i => i.InteractionID == id);
            }
            catch (Exception ex)
            {
                throw new ConflictException("Error retrieving the interaction by ID from the database", ex);

            }

        }
    }
}
