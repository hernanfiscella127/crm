using CRM.Domain.Entities;

namespace CRM.Aplication.Interfaces
{
    public interface IProjectsQuery
    {
        public Task<List<Projects>> GetProjectsByFilter(string? name, int? clientId, int? campaignTypeId, int? pageNumber, int? pageSize);
        public Task<Projects> GetProjectById(Guid projectId);
        public Task<Projects> GetProjectByName(string Name);

        //Todo: refactorizar y mover a sus respectivas clases
        public Task<Domain.Entities.TaskStatus> GetTaskStatusById(int statusId);
        public Task<Users> GetUserById(int userId);


    }
}
