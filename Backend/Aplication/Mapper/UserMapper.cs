using CRM.Aplication.Mapper.IMappers;
using CRM.Aplication.Response;
using CRM.Domain.Entities;

namespace CRM.Aplication.Mapper
{
    public class UserMapper : IUserMapper
    {
        public async Task<List<UsersResponse>> GetAllUserResponseMapper(List<Users> allUsers)
        {
            List<UsersResponse> list = new List<UsersResponse>();
            foreach (var user in allUsers)
            {
                var response = new UsersResponse
                {
                    Id = user.UserID,
                    Name = user.Name,
                    Email = user.Email
                };
                list.Add(response);
            }
            return await System.Threading.Tasks.Task.FromResult(list);
        }
        public async Task<UsersResponse> GetUserResponseMapper(Users user)
        {
            var response = new UsersResponse
            {
                Id = user.UserID,
                Name = user.Name,
                Email = user.Email

            };
            return await System.Threading.Tasks.Task.FromResult(response);
        }
    }
}
