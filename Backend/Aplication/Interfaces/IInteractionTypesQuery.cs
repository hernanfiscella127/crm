using CRM.Domain.Entities;

namespace CRM.Aplication.Interfaces
{
    public interface IInteractionTypesQuery
    {
        public Task<List<InteractionTypes>> GetAllInterationTypes();

    }
}
