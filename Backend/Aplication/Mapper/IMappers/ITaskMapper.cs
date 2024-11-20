using CRM.Aplication.Response;

namespace CRM.Aplication.Mapper.IMappers
{
    public interface ITaskMapper
    {
        public Task<List<TasksResponse>> GetAllTasksResponseMapper(List<Domain.Entities.Tasks> allTaks);
        public Task<ProjectAddTaskToResponse> GetTaksResponseMapper(Domain.Entities.Tasks task);
        public Task<UpdateTaskResponse> GetUpdatedTaksResponseMapper(Domain.Entities.Tasks task);

    }
}
