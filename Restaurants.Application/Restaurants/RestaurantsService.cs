using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.IRepos;
using Restraurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants
{
    public class RestaurantsService : IRestaurantsService
    {
        private IRestaurantsRepo _restaurantsRepo;
        private ILogger<RestaurantsService> _logger;
        private IMapper _mapper;
        public RestaurantsService(IRestaurantsRepo restaurantsRepo,
            ILogger<RestaurantsService> logger,
            IMapper mapper)
        {
            _restaurantsRepo = restaurantsRepo;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
        {
            _logger.LogInformation($"GetAllRestaurants");
            var restaurants =await _restaurantsRepo.GetAllAsync();
            var restaurantDtos = _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
            return restaurantDtos;
        }
        public async Task<RestaurantDto?> GetRestaurant( int id )
        {
            _logger.LogInformation($"Restaurant: {id}");
            var restaurant =await _restaurantsRepo.GetByIdAsync( id );
            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);
            return restaurantDto;
        }
        public async Task<int> Create(CreateRestaurantDto restaurantDto)
        {
            _logger.LogInformation("Create a new Restaurant");
            var restaurant = _mapper.Map<Restaurant>(restaurantDto);
            int id = await _restaurantsRepo.Create(restaurant);
            return id;
        }
    }
}
