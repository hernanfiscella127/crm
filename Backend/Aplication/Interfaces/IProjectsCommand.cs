using CRM.Domain.Entities;

namespace CRM.Aplication.Interfaces
{
    public interface IProjectsCommand
    {
        public Task<Guid> addProject(Projects project);
        public Task<Interactions> AddInteraction(Guid projectId, Interactions? interaction);
        public System.Threading.Tasks.Task AddTaskToProject(Domain.Entities.Tasks task);


    }
}
