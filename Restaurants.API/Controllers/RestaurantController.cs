using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Infrastructure.Authorization;
using Restraurants.Domain.Utilities;

namespace Restaurants.API.Controllers
{
    [Route("api/restaurants")]
    [ApiController]
    [Authorize]
    public class RestaurantController : ControllerBase
    {
        private IMediator _mediator;
        public RestaurantController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK ,Type = typeof(IEnumerable<RestaurantDto>))]
        //[Authorize(policy: PolicyNames.HasNationality)]
        //[Authorize(policy: PolicyNames.CreatedAtleast2Restaurants)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] GetAllRestaurantsQuery query)
        {
            var restaurants =await _mediator.Send(query);
            return Ok(restaurants);
        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RestaurantDto))]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var restaurant = await _mediator.Send(new GetByIdQuery(id));
            if (restaurant == null)
            {
                return NotFound("No Restaurant With this Id");
            }
            return Ok(restaurant);
        }
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
        {
            var isDeleted = await _mediator.Send(new DeleteRestaurantCommand(id));
            if (isDeleted)
            {
                return NoContent();
            }
            return NotFound();
        }
        [HttpPost]
        [Authorize(Roles = UserRoles.Owner)]
        public async Task<IActionResult> CreateRestaurant(CreateRestaurantCommand restaurant)
        {
            int id = await _mediator.Send(restaurant);
            return CreatedAtAction(nameof(GetById) , new { id } , null);
        }
        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateRestaurant([FromRoute] int id,UpdateRestaurantCommand command )
        {
            command.Id = id;
            var isUpdated = await _mediator.Send(command);
            if (isUpdated)
                return NoContent();
            return NotFound();
        }
    }
}
