using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.IRepos;
using Restraurants.Domain.Entities;
using Restraurants.Domain.Exceptions;
using Restraurants.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Dishes.Commands.CreateDish.CreateRestaurant
{
    public class CreateDishCommandHandler(IDishesRepo dishesRepo,
        IRestaurantsRepo restaurantsRepo,
        ILogger<CreateDishCommandHandler> logger,
        IRestaurantAuthorizationService restaurantAuthorizationService,
        IMapper mapper) : IRequestHandler<CreateDishCommand, int>
    {
        private IDishesRepo _dishesRepo = dishesRepo;
        private IRestaurantsRepo _restaurantsRepo = restaurantsRepo;
        private ILogger<CreateDishCommandHandler> _logger = logger;
        private IRestaurantAuthorizationService _restaurantAuthorizationService = restaurantAuthorizationService;
        private IMapper _mapper = mapper;

        public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Create a new dish{@DishRequest}" , request);
            var restaurant =await _restaurantsRepo.GetByIdAsync(request.RestaurantId);
            if (restaurant == null)
            {
                return 0; 
            }
            if (!_restaurantAuthorizationService.Authorize(restaurant, ResourceOperations.Update))
                throw new ForbidException();
            var dish = _mapper.Map<Dish>(request);
            return await _dishesRepo.Create(dish);
        }

        
    }
}
