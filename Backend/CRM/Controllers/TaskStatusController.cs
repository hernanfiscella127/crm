using CRM.Aplication.Interfaces;
using CRM.Aplication.Response;
using CRM.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TaskStatusController : ControllerBase
    {
        private readonly ITaskStatusService _taskStatusService;

        public TaskStatusController(ITaskStatusService taskStatusService)
        {
            _taskStatusService = taskStatusService;
        }

        [HttpGet("/api/v1/TaskStatus")]
        [ProducesResponseType(typeof(TaskStatusResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTaskStatuses()
        {
            var statuses = await _taskStatusService.GetAllTaskStatuses();
            return new JsonResult(statuses) { StatusCode = 200 };

        }
    }
}
