using CRM.Aplication.Response;

namespace CRM.Aplication.Mapper.IMappers
{
    public interface ITaskStatusMapper
    {
        Task<List<TaskStatusResponse>> GetAlTaskStatusResponseMapper(List<Domain.Entities.TaskStatus> allTaskStatus);
        public Task<TaskStatusResponse> GetTaskSatusResponseMapper(Domain.Entities.TaskStatus task);

    }
}
