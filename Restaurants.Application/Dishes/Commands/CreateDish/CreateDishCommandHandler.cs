using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.IRepos;
using Restraurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Dishes.Commands.CreateDish.CreateRestaurant
{
    public class CreateDishCommandHandler : IRequestHandler<CreateDishCommand, int>
    {
        private IDishesRepo _dishesRepo;
        private IRestaurantsRepo _restaurantsRepo;
        private ILogger<CreateDishCommandHandler> _logger;
        private IMapper _mapper;
        public CreateDishCommandHandler(IDishesRepo dishesRepo,
            IRestaurantsRepo restaurantsRepo,
            ILogger<CreateDishCommandHandler> logger,
            IMapper mapper)
        {
            _dishesRepo = dishesRepo;
            _restaurantsRepo = restaurantsRepo;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Create a new dish{@DishRequest}" , request);
            var restaurant =await _restaurantsRepo.GetByIdAsync(request.RestaurantId);
            if (restaurant == null)
            {
                return 0; 
            }
            var dish = _mapper.Map<Dish>(request);
            return await _dishesRepo.Create(dish);
        }

        
    }
}
