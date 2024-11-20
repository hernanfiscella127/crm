using CRM.Aplication.Interfaces;
using CRM.Aplication.Response;
using IUserMapper = CRM.Aplication.Mapper.IMappers.IUserMapper;

namespace CRM.Aplication.UseCases
{
    public class UserService : IUserService
    {
        private readonly IUserQuery _query;
        private readonly IUserMapper _userMapper;
        public UserService(IUserQuery userRepository, IUserMapper userMapper)
        {
            _query = userRepository;
            _userMapper = userMapper;
        }

        public async Task<List<UsersResponse>> GetUsers()
        {
            var users = await _query.GetUsers();
            return await _userMapper.GetAllUserResponseMapper(users);
        }
    }
}
