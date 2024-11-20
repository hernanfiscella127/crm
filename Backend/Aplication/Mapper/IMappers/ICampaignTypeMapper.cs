using CRM.Aplication.Response;
using CRM.Domain.Entities;

namespace CRM.Aplication.Mapper.IMappers
{
    public interface ICampaignTypeMapper
    {
        Task<List<GenericResponse>> GetAllCampaignTypeResponseMapper(List<CampaignTypes> campaings);
        public Task<GenericResponse> GetCampaignTypeResponseMapper(CampaignTypes campaings);

    }
}
