using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Commands.DeleteDishes;
using Restaurants.Application.Dishes.Queries.GetAllForRestaurant;
using Restaurants.Application.Dishes.Queries.GetByIdForRestaurant;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;

namespace Restaurants.API.Controllers
{
    [Route("api/restaurants/{restaurantId}/dishes")]
    [ApiController]
    public class DishesController : ControllerBase
    {
        private IMediator _mediator;
        public DishesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateDish([FromRoute] int restaurantId ,CreateDishCommand command)
        {
            command.RestaurantId = restaurantId;
            var dishId = await _mediator.Send(command);
            if (dishId == 0)
                return NotFound();
            return CreatedAtAction(nameof(GetByIdForRestaurant) , new {restaurantId , dishId} , null);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllForRestaurant([FromRoute]int restaurantId)
        {
            var dishes = await _mediator.Send(new GetAllForRestaurantQuery(restaurantId));
            if(dishes is  null) return NotFound();
            return Ok(dishes);
        }
        [HttpGet("{dishId}")]
        public async Task<IActionResult> GetByIdForRestaurant([FromRoute] int restaurantId , [FromRoute] int dishId)
        {
            var dish = await _mediator.Send(new GetByIdForRestaurantQuery(restaurantId , dishId));
            if(dish is null) return NotFound();
            return Ok(dish);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteDishes([FromRoute] int restaurantId)
        {
            var isDeleted = await _mediator.Send(new DeleteDishesCommand(restaurantId));
            if(isDeleted) return NoContent();
            return NotFound();
        }
    }
}
