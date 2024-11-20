using CRM.Aplication.Response;
using CRM.Domain.Entities;

namespace CRM.Aplication.Mapper.IMappers
{
    public interface IInteractionTypesMapper
    {
        public Task<List<GenericResponse>> GetAllIInteractionTypesResponseMapper(List<InteractionTypes> interactionsTypes);
        public Task<GenericResponse> GetInteractionTypeResponseMapper(InteractionTypes campaings);

    }
}
