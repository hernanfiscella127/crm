using CRM.Aplication.Interfaces;
using CRM.Aplication.Response;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CampaignTypeController : ControllerBase
    {
        private readonly ICampaignTypeService CampaignService;

        public CampaignTypeController(ICampaignTypeService cService)
        {
            CampaignService = cService;
        }

        [HttpGet("/api/v1/CampaignType")]
        [ProducesResponseType(typeof(GenericResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<GenericResponse>>> GetAllCampaignTypes()
        {
            var campaignTypes = await CampaignService.getCampaignTypes();
            return new JsonResult(campaignTypes) { StatusCode = 200 };
        }
    }
}
