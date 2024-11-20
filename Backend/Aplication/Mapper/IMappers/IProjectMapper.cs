using CRM.Aplication.Response;
using CRM.Domain.Entities;

namespace CRM.Aplication.Mapper.IMappers
{
    public interface IProjectMapper
    {
        public Task<ProjectsWithDetailsResponse> GetProjectsWithDetailsResponseMapper(Projects project);
        public Task<List<ProjectResponse>> GetAllProjectsWithOptionalFiltersResponseMapper(List<Projects> allClients);
        public Task<ProjectCreateResponse> GetProjectResponse(Projects project);


    }
}
