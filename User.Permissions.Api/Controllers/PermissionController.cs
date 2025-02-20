using MediatR;
using Microsoft.AspNetCore.Mvc;
using User.Permissions.Application.Permissions.Create;
using User.Permissions.Application.Permissions.GetAll;
using User.Permissions.Application.Permissions.Update;

namespace User.Permissions.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PermissionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePermission([FromBody] CreatePermissionCommand createPermissionCommand)
        {
            var result = await _mediator.Send(createPermissionCommand);
            if (result.IsSuccess)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePermission([FromBody] UpdatePermissionCommand updatePermissionCommand)
        {
            var result = await _mediator.Send(updatePermissionCommand);
            if (result.IsSuccess)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetPermissions()
        {
            var query = new GetAllPermissionsQuery();
            var result = await _mediator.Send(query);
            if (result.Any())
                return Ok(result);

            return NoContent();
        }
    }
}
