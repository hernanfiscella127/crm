using CRM.Aplication.Request;
using CRM.Aplication.Response;

namespace CRM.Aplication.Interfaces
{
    public interface IProjectsService
    {
        public Task<List<ProjectResponse>> SearchProjectsWithOptionalsFilters(string? name, int? clientId, int? campaignTypeId, int? pageNumber, int? pageSize);
        public Task<ProjectsWithDetailsResponse> CreateProject(ProjectRequest command);
        public Task<ProjectsWithDetailsResponse> GetProjectById(Guid id);

        //revisar si no hay que mover a otros servicios:
        public Task<InteractionsResponse> AddInteraction(Guid projectId, InteractionsRequest request);
        public Task<ProjectAddTaskToResponse> AddTaskToProject(TasksRequest command, Guid id);
        public Task<UpdateTaskResponse> UpdateTask(TasksRequest command, Guid id);

    }
}
