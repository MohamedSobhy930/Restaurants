using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Dishes.Commands.CreateDish.CreateRestaurant;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.IRepos;
using Restraurants.Domain.Exceptions;
using Restraurants.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Commands.DeleteDishes
{
    public class DeleteDishesCommandHandler(IDishesRepo dishesRepo,
        IRestaurantsRepo restaurantsRepo,
        ILogger<DeleteDishesCommandHandler> logger,
        IRestaurantAuthorizationService restaurantAuthorizationService,
        IMapper mapper) : IRequestHandler<DeleteDishesCommand, bool>
    {
        private IDishesRepo _dishesRepo = dishesRepo;
        private IRestaurantsRepo _restaurantsRepo = restaurantsRepo;
        private ILogger<DeleteDishesCommandHandler> _logger = logger;
        private IRestaurantAuthorizationService _restaurantAuthorizationService = restaurantAuthorizationService;
        private IMapper _mapper = mapper;

        public async Task<bool> Handle(DeleteDishesCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Delete Dishes For restaurant with Id {RestaurantId}", request.restaurantId);
            var restaurant =await _restaurantsRepo.GetByIdAsync(request.restaurantId);
            if (restaurant == null)
                return false;
            if (!_restaurantAuthorizationService.Authorize(restaurant, ResourceOperations.Update))
                throw new ForbidException();
            await _dishesRepo.Delete(restaurant.Dishes);
            return true;

        }
    }
}
