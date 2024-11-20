using CRM.Aplication.Mapper.IMappers;
using CRM.Aplication.Response;

namespace CRM.Aplication.Mapper
{

    public class TaskMapper : ITaskMapper
    {
        private readonly ITaskStatusMapper _taskStatusMapper;
        private readonly IUserMapper _userMapper;

        public TaskMapper(ITaskStatusMapper taskStatusMapper, IUserMapper userMapper)
        {
            _taskStatusMapper = taskStatusMapper;
            _userMapper = userMapper;
        }

        public async Task<List<TasksResponse>> GetAllTasksResponseMapper(List<Domain.Entities.Tasks> allTaks)
        {
            List<TasksResponse> list = new List<TasksResponse>();
            foreach (var task in allTaks)
            {
                var response = new TasksResponse
                {
                    Id = task.TaskID,
                    DueDate = task.DueDate,
                    Name = task.Name,

                };
                list.Add(response);
            }
            return await System.Threading.Tasks.Task.FromResult(list);
        }

        public async Task<ProjectAddTaskToResponse> GetTaksResponseMapper(Domain.Entities.Tasks task)
        {
            var response = new ProjectAddTaskToResponse
            {
                Id = task.TaskID,
                Name = task.Name,
                DueDate = task.DueDate,
                ProjectId = task.ProjectID,
                Status = await _taskStatusMapper.GetTaskSatusResponseMapper(task.TaskStatus),
                UserAssigned = await _userMapper.GetUserResponseMapper(task.User)
            };
            return response;
        }
        public async Task<UpdateTaskResponse> GetUpdatedTaksResponseMapper(Domain.Entities.Tasks task)
        {
            var response = new UpdateTaskResponse
            {
                Id = task.TaskID,
                Name = task.Name,
                DueDate = task.DueDate,
                Status = await _taskStatusMapper.GetTaskSatusResponseMapper(task.TaskStatus),
                UserAssigned = await _userMapper.GetUserResponseMapper(task.User)
            };
            return response;
        }


    }
}
