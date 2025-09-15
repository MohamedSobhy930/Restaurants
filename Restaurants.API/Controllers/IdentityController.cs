using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Users.Commands;

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
    }
}
