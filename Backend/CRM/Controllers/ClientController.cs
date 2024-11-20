using CRM.Aplication.Interfaces;
using CRM.Aplication.Request;
using CRM.Aplication.Response;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientesService;

        public ClientController(IClientService clientesService)
        {
            _clientesService = clientesService;
        }


        [HttpGet("/api/v1/Client")]
        [ProducesResponseType(typeof(List<ClientResponse>), 200)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<ClientResponse>>> GetClients()
        {
            var result = await _clientesService.GetClients();
            return new JsonResult(result)
            {
                StatusCode = 200
            };
        }

        [HttpPost("/api/v1/Client")]
        [ProducesResponseType(typeof(ClientResponse), 201)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        public async Task<IActionResult> CreateClient(CreateClientRequest client)
        {
            try
            {
                var result = await _clientesService.RegisterClient(client);
                return new JsonResult(result)
                {
                    StatusCode = 201
                };
            }catch(ArgumentException ex)
            {
                return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 400 };
            }
            

        }

    }
}
