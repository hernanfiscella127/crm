using CRM.Aplication.Response;

namespace CRM.Aplication.Interfaces
{
    public interface IUserService
    {
        public Task<List<UsersResponse>> GetUsers();

    }
}
