using CRM.Domain.Entities;

namespace CRM.Aplication.Interfaces
{
    public interface IInteractionQuery
    {
        public Task<Interactions> GetInteractionById(Guid id);

    }
}
