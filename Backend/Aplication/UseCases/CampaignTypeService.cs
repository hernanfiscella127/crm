using CRM.Aplication.Interfaces;
using CRM.Aplication.Mapper.IMappers;
using CRM.Aplication.Response;

namespace CRM.Aplication.UseCases
{
    public class CampaignTypeService : ICampaignTypeService
    {
        //private readonly ICampaignTypeCommand _command;
        private readonly ICampaingTypeQuery _query;
        private readonly ICampaignTypeMapper _campaing;

        public CampaignTypeService(ICampaingTypeQuery query, ICampaignTypeMapper campaing)
        {
            //_command = command;
            _query = query;
            _campaing = campaing;
        }

        public async Task<List<GenericResponse>> getCampaignTypes()
        {
            var campaignTypes = await _query.getAllCampaingTypes();
            return await _campaing.GetAllCampaignTypeResponseMapper(campaignTypes);

        }
    }
}
