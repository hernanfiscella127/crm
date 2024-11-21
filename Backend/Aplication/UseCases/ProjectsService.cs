using CRM.Aplication.Exceptions;
using CRM.Aplication.Interfaces;
using CRM.Aplication.Mapper.IMappers;
using CRM.Aplication.Request;
using CRM.Aplication.Response;
using CRM.Domain.Entities;
using Interaction = CRM.Domain.Entities.Interactions;

namespace CRM.Aplication.UseCases
{
    public class ProjectsService : IProjectsService
    {
        private readonly IProjectsCommand _command;
        private readonly IProjectsQuery _query;
        private readonly IProjectMapper _projectMapper;
        private readonly IClientsQuery _clientQuery;
        private readonly ICampaingTypeQuery _campaingTypeQuery;
        private readonly ITaskQuery _taskQuery;
        private readonly ITasksCommand _taskCommand;
        private readonly ITaskMapper _taskMapper;
        private readonly IInteractionMapper _interactionMapper;
        private readonly IInteractionQuery _interactionQuery;

        public ProjectsService(ITaskQuery taskQuery, ITasksCommand taskCommand, IProjectsCommand command, IProjectsQuery query,
            IClientsQuery clientQuery, ICampaingTypeQuery campaingTypeQuery, IProjectMapper projectMapper,
            IInteractionMapper interactionMapper, IInteractionQuery interactionQuer, ITaskMapper taskMapper)
        {
            _taskQuery = taskQuery;
            _taskCommand = taskCommand;
            _command = command;
            _query = query;
            _clientQuery = clientQuery;
            _campaingTypeQuery = campaingTypeQuery;
            _projectMapper = projectMapper;
            _interactionMapper = interactionMapper;
            _interactionQuery = interactionQuer;
            _taskMapper = taskMapper;
        }

        public async Task<List<ProjectResponse>> SearchProjectsWithOptionalsFilters(string? name, int? clientId, int? campaignTypeId, int? pageNumber, int? pageSize)
        {
            if (pageNumber.HasValue && pageNumber <= 0)
                throw new ArgumentException("Page number must be greater than 0.");

            if (pageSize.HasValue && pageSize <= 0)
                throw new ArgumentException("Page size must be greater than 0.");

            var projects = await _query.GetProjectsByFilter(name, clientId, campaignTypeId, pageNumber, pageSize);
            return await _projectMapper.GetAllProjectsWithOptionalFiltersResponseMapper(projects);
        }

        public async Task<ProjectsWithDetailsResponse> CreateProject(ProjectRequest command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command), "Request cannot be null.");

            if (string.IsNullOrWhiteSpace(command.Name))
                throw new ArgumentException("Projects name cannot be empty.");

            if (command.End <= DateTime.Now)
                throw new ArgumentException("EndDate date must be in the future.");

            var clientExist = await _clientQuery.GetClientById(command.Client);
            if (clientExist == null)
                throw new BadRequestException("Clients not found.");

            var campaingTypeExist = await _campaingTypeQuery.GetCampaingTypeById(command.CampaignType);
            if (campaingTypeExist == null)
                throw new BadRequestException("Campaign type not found.");

            var projectAlreadyExist = await _query.GetProjectByName(command.Name);
            if (projectAlreadyExist != null)
                throw new BadRequestException("A project with this name already exists.");

            var project = new Projects
            {
                ProjectName = command.Name,
                ClientID = command.Client,
                CampaignType = command.CampaignType,
                StartDate = DateTime.Now,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                EndDate = command.End,
            };

            await _command.addProject(project);
            var project2 = await _query.GetProjectById(project.ProjectID);
            return await _projectMapper.GetProjectsWithDetailsResponseMapper(project2);
        }

        public async Task<ProjectsWithDetailsResponse> GetProjectById(Guid id)
        {

            var project = await _query.GetProjectById(id);
            if (project == null)
                throw new BadRequestException("A project with this ID doesn't exist.");

            return await _projectMapper.GetProjectsWithDetailsResponseMapper(project);
        }

        public async Task<InteractionsResponse> AddInteraction(Guid projectId, InteractionsRequest request)
        {
            if (projectId == Guid.Empty)
                throw new ArgumentException("Invalid project ID.");

            if (request.InteractionType < 1 || request.InteractionType > 4)
            {
                throw new ArgumentException("There´s no Interaction type with that id");
            }



            if (request == null || string.IsNullOrWhiteSpace(request.Notes))
                throw new ArgumentException("Interactions request or notes cannot be null.");

            var project = await _query.GetProjectById(projectId);
            if (project == null)
                throw new ArgumentException("Projects not found.");

            var interaction = new Interaction
            {
                Notes = request.Notes,
                Date = request.Date,
                ProjectID = projectId,
                InteractionType = request.InteractionType
            };

            var re = await _command.AddInteraction(projectId, interaction);
            return await _interactionMapper.GetInteractionResponseMapper(await _interactionQuery.GetInteractionById(re.InteractionID));
        }

        public async Task<ProjectAddTaskToResponse> AddTaskToProject(TasksRequest command, Guid id)
        {
            if (command == null || id == Guid.Empty || string.IsNullOrWhiteSpace(command.Name))
                throw new ArgumentException("Request, project ID, or task name cannot be null.");

            var project = await _query.GetProjectById(id);
            if (project == null)
                throw new ArgumentException("Projects not found.");

            var status = await _query.GetTaskStatusById(command.Status);
            if (status == null)
                throw new ArgumentException("Tasks status not found.");

            var user = await _query.GetUserById(command.User);
            if (user == null)
                throw new ArgumentException("Users not found.");

            if (command.DueDate <= DateTime.Now)
                throw new ArgumentException("Due date must be in the future.");

            var task = new Tasks
            {
                TaskID = Guid.NewGuid(),
                Name = command.Name,
                DueDate = command.DueDate,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                ProjectID = id,
                Status = command.Status,
                AssignedTo = command.User
            };

            await _command.AddTaskToProject(task);
            return await _taskMapper.GetTaksResponseMapper(task);
        }

        public async Task<UpdateTaskResponse> UpdateTask(TasksRequest command, Guid id)
        {
            if (command == null || id == Guid.Empty)
                throw new ArgumentException("Request or task ID cannot be null.");

            if (string.IsNullOrWhiteSpace(command.Name))
            {
                throw new ArgumentException("Name cannot be null.");
            }

            var task = await _taskQuery.GetTaskById(id);
            if (task == null)
                throw new ArgumentException("Tasks not found.");

            var status = await _query.GetTaskStatusById(command.Status);
            if (status == null)
                throw new ArgumentException("Tasks status not found.");

            var user = await _query.GetUserById(command.User);
            if (user == null)
                throw new ArgumentException("Users not found.");

            if (command.DueDate <= DateTime.Now)
                throw new ArgumentException("Due date must be in the future.");

            task.Name = command.Name;
            task.DueDate = command.DueDate;
            task.UpdateDate = DateTime.Now;
            task.Status = command.Status;
            task.AssignedTo = command.User;

            await _taskCommand.UpdateTask(task);
            return await _taskMapper.GetUpdatedTaksResponseMapper(task);
        }
    }
}
