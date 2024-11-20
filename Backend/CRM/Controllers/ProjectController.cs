using CRM.Aplication.Exceptions;
using CRM.Aplication.Interfaces;
using CRM.Aplication.Request;
using CRM.Aplication.Response;
using Microsoft.AspNetCore.Mvc;
using BadRequest = CRM.Aplication.Request.BadRequest;

namespace CRM.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectsService _projectsService;

        public ProjectController(IProjectsService projectsService)
        {
            _projectsService = projectsService;
        }

        [HttpGet("/api/v1/Project")]
        [ProducesResponseType(typeof(List<ProjectResponse>), 200)]
        public async Task<IActionResult> GetProjects(string? name, int? campaign, int? client, int? offset, int? size)
        {
            if (offset.HasValue && offset < 0)
                return BadRequest(new { message = "Offset must be 0 or positive." });

            if (size.HasValue && size <= 0)
                return BadRequest(new { message = "Size must be greater than 0." });



            var response = await _projectsService.SearchProjectsWithOptionalsFilters(name, client, campaign, offset, size);
            return new JsonResult(response) { StatusCode = 200 };


        }

        [HttpPost("/api/v1/Project")]
        [ProducesResponseType(typeof(ProjectsWithDetailsResponse), 201)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        public async Task<IActionResult> CreateProject([FromBody] ProjectRequest command)
        {
            try
            {
                var result = await _projectsService.CreateProject(command);
                return new JsonResult(result) { StatusCode = 201 };
            }
            catch (ArgumentException ex)
            {
                return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 400 };
            }

        }

        [HttpGet("/api/v1/Project/{id}")]
        [ProducesResponseType(typeof(ProjectResponse), 200)]
        [ProducesResponseType(typeof(BadRequest), 404)]
        public async Task<IActionResult> GetProjectById(Guid id)
        {
            try
            {
                var response = await _projectsService.GetProjectById(id);

                return new JsonResult(response) { StatusCode = 200 };
            }
            catch (BadRequestException ex)
            {
                return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 404 };
            }
        }

        [HttpPatch("/api/v1/Project/{id}/interactions")]
        [ProducesResponseType(typeof(InteractionsResponse), 200)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        public async Task<IActionResult> AddInteraction(Guid id, [FromBody] InteractionsRequest request)
        {

            try
            {
                var response = await _projectsService.AddInteraction(id, request);
                return new JsonResult(response) { StatusCode = 200 };
            }
            catch (ArgumentException ex)
            {
                return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 400 };
            }

        }

        [HttpPatch("/api/v1/Project/{id}/tasks")]
        [ProducesResponseType(typeof(TasksResponse), 200)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        public async Task<IActionResult> AddTaskToProject(Guid id, [FromBody] TasksRequest request)
        {

            try
            {
                var response = await _projectsService.AddTaskToProject(request, id);
                return new JsonResult(response) { StatusCode = 200 };
            }
            catch (ArgumentException ex)
            {
                return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 400 };
            }

        }

        [HttpPut("/api/v1/Tasks/{id}")]
        [ProducesResponseType(typeof(TasksResponse), 200)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody] TasksRequest request)
        {
            try
            {
                var response = await _projectsService.UpdateTask(request, id);
                return new JsonResult(response) { StatusCode = 200 };
            }
            catch (ArgumentException ex)
            {
                return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 400 };
            }

        }
    }
}
