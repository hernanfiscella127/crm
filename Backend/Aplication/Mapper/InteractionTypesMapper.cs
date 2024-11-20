using CRM.Aplication.Mapper.IMappers;
using CRM.Aplication.Response;
using CRM.Domain.Entities;

namespace CRM.Aplication.Mapper
{
    public class InteractionTypesMapper : IInteractionTypesMapper
    {
        public async Task<List<GenericResponse>> GetAllIInteractionTypesResponseMapper(List<InteractionTypes> interactionsTypes)
        {
            List<GenericResponse> list = new List<GenericResponse>();
            foreach (var client in interactionsTypes)
            {
                var response = new GenericResponse
                {
                    Id = client.Id,
                    Name = client.Name
                };
                list.Add(response);
            }

            return await System.Threading.Tasks.Task.FromResult(list);
        }
        public async Task<GenericResponse> GetInteractionTypeResponseMapper(InteractionTypes campaings)
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
