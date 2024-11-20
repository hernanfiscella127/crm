using CRM.Aplication.Exceptions;
using CRM.Aplication.Interfaces;
using CRM.Domain.Entities;
using CRM.Infraestructure.Percistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infraestructure.Command
{
    public class ProjectsCommand : IProjectsCommand
    {
        private readonly CrmContext _context;

        public ProjectsCommand(CrmContext context)
        {
            _context = context;
        }

        public async Task<Guid> addProject(Projects project)
        {
            try
            {
                _context.Projects.Add(project);
                await _context.SaveChangesAsync();

                return project.ProjectID;
            }
            catch (DbUpdateException ex)
            {
                // Capturar la excepción interna para obtener más detalles
                var innerException = ex.InnerException?.Message;
                throw new Exception($"Error occurred while saving the entity changes: {innerException}");
            }

        }
        public async Task<Interactions> AddInteraction(Guid projectId, Interactions? interaction)
        {
            try
            {
                var project = await _context.Projects.Include(p => p.Interactions)
                                                 .FirstOrDefaultAsync(p => p.ProjectID == projectId);

                if (project == null)
                {
                    throw new Exception("Projects not found");
                }

                project.Interactions.Add(interaction);
                await _context.SaveChangesAsync();
                return interaction;
            }
            catch (DbUpdateException ex)
            {
                throw new ConflictException("There was an issue adding the interaction tho the database.", ex);
            }

        }
        //TODO: move this method to TaskCommand
        public async System.Threading.Tasks.Task AddTaskToProject(Domain.Entities.Tasks task)
        {
            try
            {
                _context.Tasks.Add(task);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new ConflictException("There was an issue adding the task to a project in the database.", ex);
            }

        }
    }
}
