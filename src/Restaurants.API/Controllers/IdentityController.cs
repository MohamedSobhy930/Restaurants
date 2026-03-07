using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Users.Commands.AssignUserRole;
using Restaurants.Application.Users.Commands.DeleteUserRole;
using Restaurants.Application.Users.Commands.UpdateUser;
using Restraurants.Domain.Utilities;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("/api/identity")]
    public class IdentityController(IMediator mediator) : ControllerBase
    {
        [HttpPatch("user")]
        [Authorize]
        public async Task<IActionResult> UpdateUserDetail(UpdateUserCommand command)
        {
            var isUpdated =await mediator.Send(command);
            if(isUpdated)
            {
                return NoContent();
            }
            return NotFound();
        }
        [HttpPost("userRole")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> AssignUserRole(AssignUserRoleCommand command)
        {
            var isAssigned = await mediator.Send(command);
            if (isAssigned)
            {
                return Ok();
            }
            return NotFound();
        }
        [HttpDelete("userRole")]
        [Authorize(Roles =UserRoles.Admin)]
        public async Task<IActionResult> UnassignUserRole(DeleteUserRoleCommand command)
        {
            var isDeleted = await mediator.Send(command);
            if (isDeleted)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
