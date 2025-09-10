using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.DTOs;

namespace Restaurants.API.Controllers
{
    [Route("api/restaurants")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantsService _restaurantsService;
        private IValidator<CreateRestaurantDto> _validator;
        public RestaurantController(IRestaurantsService restaurantsService,
            IValidator<CreateRestaurantDto> validator) 
        {
            _restaurantsService = restaurantsService;
            _validator = validator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var restaurants =await _restaurantsService.GetAllRestaurants();
            return Ok(restaurants);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var restaurant = await _restaurantsService.GetRestaurant(id);
            if (restaurant == null)
            {
                return NotFound("No Restaurant With this Id");
            }
            return Ok(restaurant);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRestaurant([FromBody]CreateRestaurantDto restaurantDto)
        {
            var validationResult = await _validator.ValidateAsync(restaurantDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToDictionary());
            }
            int id = await _restaurantsService.Create(restaurantDto);
            return CreatedAtAction(nameof(GetById) , new { id } , null);
        }
    }
}
