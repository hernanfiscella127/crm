using CRM.Aplication.Exceptions;
using CRM.Aplication.Interfaces;
using CRM.Infraestructure.Percistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infraestructure.Command
{
    public class TasksCommand : ITasksCommand
    {
        private readonly CrmContext _context;

        public TasksCommand(CrmContext context)
        {
            _context = context;
        }

        public async Task UpdateTask(Domain.Entities.Tasks task)
        {
            try
            {
                _context.Tasks.Update(task);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new ConflictException("There was an issue updating the existing task in the database.", ex);
            }

        }
    }
}
