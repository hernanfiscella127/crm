using CRM.Aplication.Interfaces;
using CRM.Aplication.Response;
using CRM.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class InteractionTypesController : ControllerBase
    {
        private readonly IInteractionTypesService _interactionTypesService;

        public InteractionTypesController(IInteractionTypesService _interactionTypes)
        {
            _interactionTypesService = _interactionTypes;
        }

        [HttpGet("/api/v1/InteractionTypes")]
        [ProducesResponseType(typeof(List<GenericResponse>), 200)]
        public async Task<ActionResult<List<GenericResponse>>> GetAllInteractionTypes()
        {
            var interactionTypes = await _interactionTypesService.ObteinAllInteractionTypes();
            return new JsonResult(interactionTypes) { StatusCode = 200 };
        }

    }
}
