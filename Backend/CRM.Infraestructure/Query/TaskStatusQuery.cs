using CRM.Aplication.Exceptions;
using CRM.Aplication.Interfaces;
using CRM.Infraestructure.Percistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infrastructure.Query
{
    public class TaskStatusQuery : ITaskStatusQuery
    {
        private readonly CrmContext _context;

        public TaskStatusQuery(CrmContext context)
        {
            _context = context;
        }

        public async Task<List<Domain.Entities.TaskStatus>> GetAllTaskStatusesAsync()
        {
            try
            {
                var taskStatuses = await _context.TaskStatuses.ToListAsync();
                return taskStatuses;
            }
            catch (DbUpdateException ex)
            {
                throw new ConflictException("There was an issue accessing all the task statuses in the database.", ex);
            }
        }
    }
}

