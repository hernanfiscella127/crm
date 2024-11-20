using CRM.Aplication.Response;
using CRM.Domain.Entities;

namespace CRM.Aplication.Mapper.IMappers
{
    public interface IInteractionMapper
    {
        public Task<List<InteractionsResponse>> GetAllInteractionResponseMapper(List<Interactions> allInteractions);

        Task<InteractionsResponse> GetInteractionResponseMapper(Interactions interaction);

    }
}
