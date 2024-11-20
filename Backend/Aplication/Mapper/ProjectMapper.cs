using CRM.Aplication.Mapper.IMappers;
using CRM.Aplication.Response;
using CRM.Domain.Entities;

namespace CRM.Aplication.Mapper
{
    public class ProjectMapper : IProjectMapper
    {
        private readonly IClientMapper _clientMapper;
        private readonly ICampaignTypeMapper _campaingTypeMapper;
        private readonly IInteractionMapper _interactionMapper;
        private readonly ITaskMapper _taskMapper;

        public ProjectMapper(IClientMapper clienteMapper, ICampaignTypeMapper campaingTypeMapper, IInteractionMapper interactionMapper, ITaskMapper taskMapper)
        {
            _clientMapper = clienteMapper;
            _campaingTypeMapper = campaingTypeMapper;
            _interactionMapper = interactionMapper;
            _taskMapper = taskMapper;
        }

        public async Task<List<ProjectResponse>> GetAllProjectsWithOptionalFiltersResponseMapper(List<Projects> allProjects)
        {
            List<ProjectResponse> list = new List<ProjectResponse>();
            foreach (var project in allProjects)
            {
                var response = new ProjectResponse
                {
                    Id = project.ProjectID,
                    Name = project.ProjectName,
                    StartDate = project.StartDate,
                    EndDate = project.EndDate,
                    Client = await _clientMapper.GetClientResponseMapper(project.ClientRelation),
                    CampaignType = await _campaingTypeMapper.GetCampaignTypeResponseMapper(project.CampaignTypeNavigation)
                };
                list.Add(response);
            }
            return list;
        }

        public async Task<ProjectsWithDetailsResponse> GetProjectsWithDetailsResponseMapper(Projects project)
        {

            var response = new ProjectsWithDetailsResponse
            {
                Data = await GetProjectResponse(project),
                Interactions = await _interactionMapper.GetAllInteractionResponseMapper(project.Interactions),
                Tasks = await _taskMapper.GetAllTasksResponseMapper(project.Tasks)

            };


            return response;

        }
        public async Task<ProjectCreateResponse> GetProjectResponse(Projects project)
        {
            var response = new ProjectCreateResponse
            {
                Id = project.ProjectID,
                Name = project.ProjectName,
                Start = project.StartDate,
                End = project.EndDate,
                Client = await _clientMapper.GetClientResponseMapper(project.ClientRelation),
                CampaignType = await _campaingTypeMapper.GetCampaignTypeResponseMapper(project.CampaignTypeNavigation)
            };
            return response;
        }
    }
}
