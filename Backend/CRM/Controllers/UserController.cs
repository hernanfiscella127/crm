using CRM.Aplication.Interfaces;
using CRM.Aplication.Response;
using CRM.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _usuarioService;

        public UserController(IUserService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet("/api/v1/Users")]
        [ProducesResponseType(typeof(List<UsersResponse>), 200)]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _usuarioService.GetUsers();
            return new JsonResult(users) { StatusCode = 200 };
        }
    }
}
