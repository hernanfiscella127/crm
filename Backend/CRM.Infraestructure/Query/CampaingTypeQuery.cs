using CRM.Aplication.Exceptions;
using CRM.Aplication.Interfaces;
using CRM.Domain.Entities;
using CRM.Infraestructure.Percistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infraestructure.Query
{
    public class CampaingTypeQuery : ICampaingTypeQuery
    {
        private readonly CrmContext _context;

        public CampaingTypeQuery(CrmContext context)
        {
            _context = context;
        }

        public async Task<List<CampaignTypes>> getAllCampaingTypes()
        {
            return await _context.CampaignTypes.ToListAsync();
        }
        public async Task<CampaignTypes?> GetCampaingTypeById(int CampaingTypeId)
        {
            try
            {
                return await _context.CampaignTypes
                    .FirstOrDefaultAsync(c => c.Id == CampaingTypeId);
            }
            catch (Exception ex)
            {
                throw new ConflictException("Error retrieving the campaing types from the database", ex);
            }
        }
    }
}
