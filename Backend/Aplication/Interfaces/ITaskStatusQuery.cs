namespace CRM.Aplication.Interfaces
{
    public interface ITaskStatusQuery
    {
        public Task<List<Domain.Entities.TaskStatus>> GetAllTaskStatusesAsync();

    }
}
