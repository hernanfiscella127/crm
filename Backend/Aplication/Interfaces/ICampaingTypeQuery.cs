using CRM.Domain.Entities;

namespace CRM.Aplication.Interfaces
{
    public interface ICampaingTypeQuery
    {
        public Task<List<CampaignTypes>> getAllCampaingTypes();
        public Task<CampaignTypes?> GetCampaingTypeById(int CampaingTypeId);

    }
}
