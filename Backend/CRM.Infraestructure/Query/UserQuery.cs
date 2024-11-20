using CRM.Aplication.Exceptions;
using CRM.Aplication.Interfaces;
using CRM.Domain.Entities;
using CRM.Infraestructure.Percistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infraestructure.Query
{
    public class UserQuery : IUserQuery
    {
        private readonly CrmContext _context;

        public UserQuery(CrmContext context)
        {
            _context = context;
        }

        public async Task<List<Users>> GetUsers()
        {
            try
            {
                var users = await _context.Users.ToListAsync();
                return users;
            }
            catch (DbUpdateException ex)
            {
                throw new ConflictException("There was an issue accessing the users in the database.", ex);
            }
        }
    }
}
