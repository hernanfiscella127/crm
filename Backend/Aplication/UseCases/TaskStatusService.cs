using CRM.Aplication.Interfaces;
using CRM.Aplication.Mapper.IMappers;
using CRM.Aplication.Response;

namespace CRM.Aplication.UseCases
{
    public class TaskStatusService : ITaskStatusService
    {
        private readonly ITaskStatusQuery _query;
        private readonly ITaskStatusMapper _taskStatusMapper;
        public TaskStatusService(ITaskStatusQuery taskStatusQuery, ITaskStatusMapper taskStatusMapper)
        {
            _query = taskStatusQuery;
            _taskStatusMapper = taskStatusMapper;
        }

        public async Task<List<TaskStatusResponse>> GetAllTaskStatuses()
        {
            var taskStatuses = await _query.GetAllTaskStatusesAsync();
            return await _taskStatusMapper.GetAlTaskStatusResponseMapper(taskStatuses);

        }
    }
}
