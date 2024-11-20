using CRM.Aplication.Response;

namespace CRM.Aplication.Interfaces
{
    public interface ITaskStatusService
    {
        public Task<List<TaskStatusResponse>> GetAllTaskStatuses();

    }
}
