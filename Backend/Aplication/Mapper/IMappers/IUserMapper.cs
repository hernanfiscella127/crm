using CRM.Aplication.Response;
using CRM.Domain.Entities;

namespace CRM.Aplication.Mapper.IMappers
{
    public interface IUserMapper
    {
        Task<List<UsersResponse>> GetAllUserResponseMapper(List<Users> allClients);
        public Task<UsersResponse> GetUserResponseMapper(Users user);

    }
}
