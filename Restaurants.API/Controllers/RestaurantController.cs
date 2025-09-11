using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;

namespace Restaurants.API.Controllers
{
    [Route("api/restaurants")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private IMediator _mediator;
        public RestaurantController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var restaurants =await _mediator.Send(new GetAllRestaurantsQuery());
            return Ok(restaurants);
        }
        [HttpGet("{id:int}")]
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
        public async Task<IActionResult> CreateRestaurant(CreateRestaurantCommand restaurant)
        {
            int id = await _mediator.Send(restaurant);
            return CreatedAtAction(nameof(GetById) , new { id } , null);
        }
        [HttpPatch("{id:int}")]
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
