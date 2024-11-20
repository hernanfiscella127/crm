using CRM.Aplication.Exceptions;
using CRM.Aplication.Interfaces;
using CRM.Infraestructure.Percistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infraestructure.Query
{
    public class TaskQuery : ITaskQuery
    {
        private readonly CrmContext _context;
        public TaskQuery(CrmContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Tasks> GetTaskById(Guid id)
        {
            try
            {
                var result = await _context.Tasks
                .Include(t => t.TaskStatus)
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.TaskID == id);

                return result;
            }
            catch (DbUpdateException ex)
            {
                throw new ConflictException("There was an issue accessing the task status by id in the database.", ex);
            }

        }


    }
}
