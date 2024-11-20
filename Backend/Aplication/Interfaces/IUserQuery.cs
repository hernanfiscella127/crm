using CRM.Domain.Entities;

namespace CRM.Aplication.Interfaces
{
    public interface IUserQuery
    {
        public Task<List<Users>> GetUsers();

    }
}
