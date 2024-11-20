using CRM.Aplication.Exceptions;
using CRM.Aplication.Interfaces;
using CRM.Domain.Entities;
using CRM.Infraestructure.Percistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infraestructure.Query
{
    public class InterationTypesQuery : IInteractionTypesQuery
    {
        private readonly CrmContext _context;

        public InterationTypesQuery(CrmContext context)
        {
            _context = context;
        }
        public async Task<List<InteractionTypes>> GetAllInterationTypes()
        {
            try
            {
                return await _context.InteractionTypes.ToListAsync();

            }
            catch (Exception ex)
            {
                throw new ConflictException("Error retrieving the interactions from the database.", ex);

            }
        }
    }
}
