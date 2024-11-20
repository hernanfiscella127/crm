using CRM.Aplication.Mapper.IMappers;
using CRM.Aplication.Response;

namespace CRM.Aplication.Mapper
{
    public class TaskStatusMapper : ITaskStatusMapper
    {
        public async Task<List<TaskStatusResponse>> GetAlTaskStatusResponseMapper(List<Domain.Entities.TaskStatus> allTaskStatus)
        {
            List<TaskStatusResponse> list = new List<TaskStatusResponse>();
            foreach (var taksS in allTaskStatus)
            {
                var response = new TaskStatusResponse
                {
                    Id = taksS.Id,
                    Name = taksS.Name
                };
                list.Add(response);
            }
            return await System.Threading.Tasks.Task.FromResult(list);
        }

        public async Task<TaskStatusResponse> GetTaskSatusResponseMapper(Domain.Entities.TaskStatus task)
        {
            var response = new TaskStatusResponse
            {
                Id = task.Id,
                Name = task.Name,

            };
            return response;
        }
    }
}
