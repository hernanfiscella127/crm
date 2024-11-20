using CRM.Aplication.Mapper.IMappers;
using CRM.Aplication.Response;
using CRM.Domain.Entities;

namespace CRM.Aplication.Mapper
{
    public class CampaignTypeMapper : ICampaignTypeMapper
    {
        public async Task<List<GenericResponse>> GetAllCampaignTypeResponseMapper(List<CampaignTypes> campaings)
        {

            List<GenericResponse> list = new List<GenericResponse>();
            foreach (var campain in campaings)
            {
                var response = new GenericResponse
                {
                    Id = campain.Id,
                    Name = campain.Name

                };
                list.Add(response);
            }
            return await System.Threading.Tasks.Task.FromResult(list);

        }
        public async Task<GenericResponse> GetCampaignTypeResponseMapper(CampaignTypes campaings)
        {
            var response = new GenericResponse
            {
                Id = campaings.Id,
                Name = campaings.Name

            };

            return await System.Threading.Tasks.Task.FromResult(response);

        }
    }
}
