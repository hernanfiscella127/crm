using CRM.Aplication.Exceptions;
using CRM.Aplication.Interfaces;
using CRM.Domain.Entities;
using CRM.Infraestructure.Percistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infraestructure.Query
{
    public class ProjectsQuery : IProjectsQuery
    {
        private readonly CrmContext _context;

        public ProjectsQuery(CrmContext context)
        {
            _context = context;
        }

        public async Task<List<Projects>> GetProjectsByFilter(string? name, int? clientId, int? campaignTypeId, int? pageNumber, int? pageSize)
        {
            try
            {
                var query = _context.Projects
                        .Include(p => p.ClientRelation)
                        .Include(p => p.CampaignTypeNavigation)
                        .AsQueryable();

                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(p => p.ProjectName.Contains(name));
                }

                if (clientId.HasValue)
                {
                    query = query.Where(p => p.ClientID == clientId.Value);
                }

                if (campaignTypeId.HasValue)
                {
                    query = query.Where(p => p.CampaignType == campaignTypeId.Value);
                }

                if (pageSize.HasValue)
                {
                    query = query.Take(pageSize.Value);
                }
                if (pageNumber.HasValue)
                {
                    query = query.Skip(pageNumber.Value);
                }


                return await query.ToListAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new ConflictException("Database error: There's been an issue accessing the projects using filters.", ex);
            }
        }
        public async Task<Projects> GetProjectById(Guid projectId)
        {
            try
            {
                var project = await _context.Projects
                    .Include(p => p.ClientRelation)
                    .Include(p => p.CampaignTypeNavigation)
                    .Include(p => p.Tasks)
                    .Include(p => p.Interactions)
                    .FirstOrDefaultAsync(p => p.ProjectID == projectId);

                return project;
            }
            catch (DbUpdateException ex)
            {
                throw new ConflictException("Database error: There's been an issue accessing the project.", ex);
            }
        }
        public async Task<Projects> GetProjectByName(string Name)
        {
            try
            {
                var existingProject = await _context.Projects
                .Where(p => p.ProjectName.ToLower() == Name.ToLower())
                .FirstOrDefaultAsync();

                return existingProject;
            }
            catch (DbUpdateException ex)
            {
                throw new ConflictException("There was an issue accessing the projects by name in the database.", ex);
            }

        }


        //TOO: mover task y user.
        public async Task<Domain.Entities.TaskStatus> GetTaskStatusById(int statusId)
        {
            try
            {
                return await _context.TaskStatuses.FindAsync(statusId);
            }
            catch (DbUpdateException ex)
            {
                throw new ConflictException("There was an issue accessing the task status by id in the database.", ex);
            }
        }

        public async Task<Users> GetUserById(int userId)
        {
            try
            {
                return await _context.Users.FindAsync(userId);
            }
            catch (DbUpdateException ex)
            {
                throw new ConflictException("There was an issue accessing the user by id in the database.", ex);
            }
        }

    }
}
