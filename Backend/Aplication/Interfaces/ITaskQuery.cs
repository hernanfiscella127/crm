namespace CRM.Aplication.Interfaces
{
    public interface ITaskQuery
    {
        public Task<Domain.Entities.Tasks> GetTaskById(Guid id);

    }
}
