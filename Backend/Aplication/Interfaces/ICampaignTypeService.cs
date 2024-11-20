using CRM.Aplication.Response;

namespace CRM.Aplication.Interfaces
{
    public interface ICampaignTypeService
    {
        public Task<List<GenericResponse>> getCampaignTypes();

    }
}
